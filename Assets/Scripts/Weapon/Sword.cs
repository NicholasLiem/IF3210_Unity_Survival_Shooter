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
        public GameObject grenade;
        public float grenadeSpeed = 200f;
        public float grenadeFireDelay = 0.5f;
        // public float range = 1f;

        SphereCollider attackRange;

        float timer;
        // Ray shootRay;
        // RaycastHit shootHit;
        int shootableMask;
        // ParticleSystem gunParticles;
        // LineRenderer gunLine;
        // AudioSource gunAudio;
        // public Light faceLight;
        // Light gunLight;
        float effectsDisplayTime = 0.2f;
        int grenadeStock = 99;

        private UnityAction listener;

        void Awake()
        {
            shootableMask = LayerMask.GetMask("Shootable", "Floor");
            attackRange = GetComponent<SphereCollider>();
            // gunParticles = GetComponent<ParticleSystem>();
            // gunLine = GetComponent<LineRenderer>();
            // gunAudio = GetComponent<AudioSource>();
            // gunLight = GetComponent<Light>();
            AdjustGrenadeStock(0);

            listener = new UnityAction(CollectGrenade);

            EventManager.StartListening("GrenadePickup", CollectGrenade);
            StartPausible();
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

            if (Input.GetButton("Fire1") && timer >= timeBetweenAttack)
            {
                Shoot();
            }

            // if (timer >= timeBetweenAttack * effectsDisplayTime)
            // {
            // DisableEffect();
            // }
        }

        void Shoot()
        {
            timer = 0f;

            Debug.Log("Attack");

            // Get all colliders within the sphere's radius
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange.radius, shootableMask);


            foreach (var hitCollider in hitColliders)
            {
                Debug.Log(hitCollider);
                // Check if the collider is an enemy
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
                Debug.Log(enemyHealth);
                if (enemyHealth != null)
                {
                    Debug.Log(enemyHealth);
                    // Calculate the direction to the enemy
                    Vector3 toEnemy = (hitCollider.transform.position - transform.position).normalized;

                    // Check if the enemy is in front of the player
                    if (Vector3.Dot(toEnemy, transform.forward) > 0)
                    {
                        enemyHealth.TakeDamage(damagePerHit, toEnemy);
                    }
                }
            }
        }


        // public void DisableEffect()
        // {
        // gunLine.enabled = false;
        // gunLight.enabled = false;
        //}
    }

}
