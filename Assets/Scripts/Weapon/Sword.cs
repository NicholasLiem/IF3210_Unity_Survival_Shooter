using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Nightmare
{
    public class Sword : PausibleObject
    {
        public int damagePerHit = 10;
        public float timeBetweenAttack = 0.3f;
        public float multiplier = 1f;

        SphereCollider attackRange;

        float timer;
        int shootableMask;
        AudioSource swordAudio;
        ParticleSystem hitParticles;

        public bool heldByPlayer = false;

        // ParticleSystem gunParticles;
        // LineRenderer gunLine;
        // AudioSource gunAudio;
        // public Light faceLight;
        // Light gunLight;
        float effectsDisplayTime = 0.2f;
        int grenadeStock = 99;
        bool oneHitKillCheat = false;

        private UnityAction listener;

        void Awake()
        {
            shootableMask = LayerMask.GetMask("Shootable", "Floor");
            attackRange = GetComponent<SphereCollider>();
            swordAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();

            StartPausible();
        }

        public void OneHitKillCheat()
        {
            oneHitKillCheat = true;
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
            timer += Time.deltaTime;
            if (heldByPlayer)
            {
                if (Input.GetButton("Fire1") && timer >= timeBetweenAttack)
                {
                    ShootPlayer();
                }
            }
            else
            {
                if (timer >= timeBetweenAttack)
                {
                    ShootEnemy();
                }
            }
        }

        void ShootPlayer()
        {
            // play sword audio
            hitParticles.Play();
            swordAudio.Play();
            timer = 0f;

            Debug.Log("Attack");

            // Get all colliders within the sphere's radius
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange.radius, shootableMask);
            GameEventsManager.instance.playerActionEvents.TriggerShotFired();

            foreach (var hitCollider in hitColliders)
            {
                Debug.Log(hitCollider);
                // Check if the collider is an enemy
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
                BufferHealth bufferPetHealth = hitCollider.GetComponent<BufferHealth>();

                Debug.Log(enemyHealth);
                if (enemyHealth != null)
                {
                    int damage = damagePerHit;

                    if (oneHitKillCheat)
                    {
                        damage = enemyHealth.currentHealth;
                    }
                    Debug.Log(enemyHealth);
                    // Calculate the direction to the enemy
                    Vector3 toEnemy = (hitCollider.transform.position - transform.position).normalized;

                    // Check if the enemy is in front of the player
                    if (Vector3.Dot(toEnemy, transform.forward) > 0)
                    {
                        GameEventsManager.instance.playerActionEvents.TriggerShotHit(true);
                        enemyHealth.TakeDamage((int)(multiplier * damage), toEnemy);
                    }
                }

                // If the BufferHealth component exist
                if (bufferPetHealth != null)
                {
                    Vector3 toEnemy = (hitCollider.transform.position - transform.position).normalized;

                    // Check if the buffer pet is in front of the player
                    if (Vector3.Dot(toEnemy, transform.forward) > 0)
                    {
                        bufferPetHealth.TakeDamage((int)(multiplier * damagePerHit));
                    }

                }
            }
        }

        void ShootEnemy()
        {
            timer = 0f;

            Debug.Log("Attack");

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange.radius, shootableMask);

            foreach (var hitCollider in hitColliders)
            {
                Debug.Log(hitCollider);
                PlayerHealth playerHealth = hitCollider.gameObject.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    Vector3 toPlayer = (hitCollider.transform.position - transform.position).normalized;
                    // Check if the enemy is in front of the player
                    if (Vector3.Dot(toPlayer, transform.forward) > 0 && !playerHealth.isDead)
                    {
                        hitParticles.Play();
                        swordAudio.Play();
                        playerHealth.TakeDamage((int)(multiplier * damagePerHit));
                    }
                }
            }
        }
    }
}
