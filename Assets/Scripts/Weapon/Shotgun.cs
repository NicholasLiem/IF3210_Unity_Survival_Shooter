using System;
using UnityEngine;
using UnityEngine.Events;

namespace Nightmare
{
    public class Shotgun : PausibleObject
    {
        public int damagePerShot = 20;
        public float timeBetweenBullets = 0.15f;
        public float range = 100f;
        public GameObject grenade;
        public float grenadeSpeed = 200f;
        public float grenadeFireDelay = 0.5f;

        float timer;
        Ray shootRay = new Ray();
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
            // Set up the references and layer mask
            shootableMask = LayerMask.GetMask("Shootable", "Floor");
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

            if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
            {
                Shoot();
            }

            if (timer >= timeBetweenBullets * effectsDisplayTime)
            {
                DisableEffects();
            }
        }

        void Shoot()
        {
            timer = 0f;
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
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Shootable"))
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
                float distance = Vector3.Distance(transform.position, hit.point);
                int finalDamage = (int)(damagePerShot * (1 - distance / range));
                enemyHealth.TakeDamage(finalDamage, hit.point);
            }

            if (bufferPetHealth != null)
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                int finalDamage = (int)(damagePerShot * (1 - distance / range));
                bufferPetHealth.TakeDamage(finalDamage);
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
    }
}
