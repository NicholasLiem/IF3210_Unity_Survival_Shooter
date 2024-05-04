using UnityEngine;
using UnityEngine.Events;
using System;
using UnitySampleAssets.CrossPlatformInput;

namespace Nightmare
{
    public class Gun : PausibleObject
    {
        public int damagePerShot = 20;
        public float timeBetweenBullets = 0.15f;
        public float range = 100f;
        public GameObject grenade;
        public float grenadeSpeed = 200f;
        public float grenadeFireDelay = 0.5f;

        float timer;
        Ray shootRay = new Ray();
        RaycastHit shootHit;
        int shootableMask;
        ParticleSystem gunParticles;
        LineRenderer gunLine;
        AudioSource gunAudio;
        Light gunLight;
        public Light faceLight;
        float effectsDisplayTime = 0.2f;
        int grenadeStock = 99;

        private UnityAction listener;

        void Awake()
        {
            shootableMask = LayerMask.GetMask("Shootable");
            gunParticles = GetComponent<ParticleSystem>();
            gunLine = GetComponent<LineRenderer>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();

            AdjustGrenadeStock(0);
            listener = new UnityAction(CollectGrenade);
            EventManager.StartListening("GrenadePickup", CollectGrenade);
            StartPausible();
        }

        void OnDestroy()
        {
            EventManager.StopListening("GrenadePickup", CollectGrenade);
            StopPausible();
        }

        void Update()
        {
            if (isPaused)
                return;

            timer += Time.deltaTime;

#if !MOBILE_INPUT
            if (timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                if (Input.GetButton("Fire2") && grenadeStock > 0)
                {
                    ShootGrenade();
                }
                else if (Input.GetButton("Fire1"))
                {
                    Shoot();
                }
            }
#else
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            {
                Shoot();
            }
#endif

            if (timer >= timeBetweenBullets * effectsDisplayTime)
            {
                DisableEffects();
            }
        }

        void Shoot()
        {
            timer = 0f;
            GameEventsManager.instance.playerActionEvents.TriggerShotFired();
            gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            RaycastHit[] hits = Physics.RaycastAll(shootRay, range, shootableMask);
            Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

            bool hitTarget = false;
            foreach (var hit in hits)
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                BufferHealth bufferPetHealth = hit.collider.GetComponent<BufferHealth>();
                if (enemyHealth != null || bufferPetHealth != null)
                {
                    ProcessHit(hit);
                    hitTarget = true;
                    break;
                }
            }

            if (!hitTarget)
            {
                GameEventsManager.instance.playerActionEvents.TriggerShotHit(false);
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }

        void ProcessHit(RaycastHit hit)
        {
            GameEventsManager.instance.playerActionEvents.TriggerShotHit(true);
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            BufferHealth bufferPetHealth = hit.collider.GetComponent<BufferHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, hit.point);
            }

            if (bufferPetHealth != null)
            {
                bufferPetHealth.TakeDamage(damagePerShot);
            }

            gunLine.SetPosition(1, hit.point);
        }

        void DisableEffects()
        {
            gunLine.enabled = false;
            gunLight.enabled = false;
        }

        public void CollectGrenade()
        {
            AdjustGrenadeStock(1);
        }

        private void AdjustGrenadeStock(int change)
        {
            grenadeStock += change;
            GrenadeManager.grenades = grenadeStock;
        }

        void ShootGrenade()
        {
            AdjustGrenadeStock(-1);
            timer = timeBetweenBullets - grenadeFireDelay;
            GameObject clone = PoolManager.Pull("Grenade", transform.position, Quaternion.identity);
            EventManager.TriggerEvent("ShootGrenade", grenadeSpeed * transform.forward);
        }
    }
}
