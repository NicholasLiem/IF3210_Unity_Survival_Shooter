using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Nightmare
{
    public class Shotgun : PausibleObject
    {
        public int damagePerShot = 20;
        public float timeBetweenBullets = 0.35f;
        public float range = 10f;
        public float multiplier = 1f;

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

        public bool heldByPlayer = false;
        bool oneShotKillCheat = false;

        PlayerHealth playerHealth;

        void Awake()
        {
            shootableMask = LayerMask.GetMask("Shootable", "Floor");
            gunParticles = GetComponent<ParticleSystem>();
            gunLine = GetComponent<LineRenderer>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();

            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

            StartPausible();
        }

        public void OneShotKillCheat()
        {
            oneShotKillCheat = true;
        }

        void OnDestroy()
        {
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


            if (heldByPlayer)
            {
                if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
                {
                    Shoot();
                }
            }
            else
            {
                if (timer >= timeBetweenBullets && !playerHealth.isDead)
                {
                    Shoot();
                }
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
            if (WeaponHelper.IsPartOfPlayer(transform))
            {
                GameEventsManager.Instance.playerActionEvents.TriggerShotFired();
            }

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                BufferHealth bufferPetHealth = shootHit.collider.GetComponent<BufferHealth>();
                PlayerHealth playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
                if (heldByPlayer)
                {
                    if (enemyHealth != null)
                    {
                        float distance = Vector3.Distance(transform.position, shootHit.point);
                        int finalDamage = (int)(damagePerShot * (1 - distance / range));
                        if (oneShotKillCheat)
                        {
                            finalDamage = enemyHealth.currentHealth;
                        }
                        Debug.Log("This is final damage " + finalDamage);
                        if (WeaponHelper.IsPartOfPlayer(transform))
                        {
                            GameEventsManager.Instance.playerActionEvents.TriggerShotHit(true);
                        }
                        enemyHealth.TakeDamage((int)(multiplier * finalDamage), shootHit.point);
                    }

                    // If the BufferHealth component exist
                    if (bufferPetHealth != null)
                    {
                        float distance = Vector3.Distance(transform.position, shootHit.point);
                        int finalDamage = (int)(damagePerShot * (1 - distance / range));
                        if (oneShotKillCheat)
                    {
                        finalDamage = enemyHealth.currentHealth;
                    }
                    bufferPetHealth.TakeDamage((int)(multiplier * finalDamage));
                    }
                }
                else
                {
                    if (playerHealth != null)
                    {
                        float distance = Vector3.Distance(transform.position, shootHit.point);
                        int finalDamage = (int)(damagePerShot * (1 - distance / range));
                        playerHealth.TakeDamage((int)(multiplier * finalDamage));
                    }
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
