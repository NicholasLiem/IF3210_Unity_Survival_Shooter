using System.Collections;
using System.Collections.Generic;
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


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
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

        public void CollectGrenade()
        {
            AdjustGrenadeStock(1);
        }

        private void AdjustGrenadeStock(int change)
        {
            grenadeStock += change;
            GrenadeManager.grenades = grenadeStock;
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

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                BufferHealth bufferPetHealth = shootHit.collider.GetComponent<BufferHealth>();

                if (enemyHealth != null)
                {
                    float distance = Vector3.Distance(transform.position, shootHit.point);
                    int finalDamage = (int)(damagePerShot * (1 - distance / range));
                    Debug.Log("This is final damage " + finalDamage);
                    enemyHealth.TakeDamage(finalDamage, shootHit.point);
                }

                // If the BufferHealth component exist
                if (bufferPetHealth != null)
                {
                    float distance = Vector3.Distance(transform.position, shootHit.point);
                    int finalDamage = (int)(damagePerShot * (1 - distance / range));
                    bufferPetHealth.TakeDamage(finalDamage);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }

        public void DisableEffects()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
            // faceLight.enabled = false;
            gunLight.enabled = false;
        }
    }
}
