﻿using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyAttack : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;
        public string petTag = "Pet";

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        PetHealth petHealth;
        EnemyHealth enemyHealth;
        bool attackableInRange;
        float timer;
        float multiplier = 1.0f;

        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent<Animator>();


            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter Enemy Attack");
            // If the entering collider is the player...
            if (other.gameObject == player || other.CompareTag(petTag))
            {
                // ... the player or his pet is in range.
                if (other.CompareTag(petTag))
                {
                    Debug.Log("[ENEMY] Found pet");
                    petHealth = other.gameObject.GetComponent<PetHealth>();
                }
                attackableInRange = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            Debug.Log("OnTriggerExit Enemy Attack");
            // If the exiting collider is the player...
            if (other.gameObject == player || other.CompareTag(petTag))
            {
                // ... the player or his pet is no longer in range.
                if (other.CompareTag(petTag))
                {
                    petHealth = null;
                }
                attackableInRange = false;
            }
        }

        void Update()
        {
            if (isPaused)
                return;

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks && attackableInRange && enemyHealth.CurrentHealth() > 0)
            {
                // ... attack.
                Debug.Log("ATTACKING ");
                Attack();
            }

            // If the player has zero or less health...
            if (playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger("PlayerDead");
            }
        }

        void Attack()
        {
            // Reset the timer.
            timer = 0f;

            Debug.Log("[ENEMY] Damage given: " + (int)(attackDamage * multiplier));
            Debug.Log("[ENEMY] Multiplier: " + multiplier);

            // Prioritize pet
            if (petHealth != null && petHealth.CurrentHealth() > 0)
            {
                Debug.Log("[ENEMY] Attacking PET");
                petHealth.TakeDamage((int)(attackDamage * multiplier));
                return;
            }

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerHealth.TakeDamage((int)(attackDamage * multiplier));
            }
        }

        public void IncreaseDamage(float percent)
        {
            Debug.Log("[ENEMY] Increasing damage: " + percent);
            multiplier += percent;
        }

        public void DecreaseDamage(float percent)
        {
            multiplier -= percent;
        }
    }
}