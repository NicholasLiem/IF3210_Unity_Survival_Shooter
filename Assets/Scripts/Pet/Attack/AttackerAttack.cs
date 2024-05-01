using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAttack : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 50;

    bool enemyInRange;
    float timer;
    GameObject enemy;
    Animator anim;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && enemyInRange && enemy != null && enemyHealth != null)
        {
            if (enemyHealth.CurrentHealth() > 0)
            {
                // ... attack.
                Attack();

            }

            if (enemyHealth.CurrentHealth() <= 0)
            {
                anim.SetBool("IsAttacking", false);
            }
        }

        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            // Kill the pet
            anim.SetTrigger("Dead");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the enemy...
        if (other.CompareTag(enemyTag))
        {
            // ... the enemy is in range. Target the enemy
            enemy = other.gameObject;
            enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the enemy...
        if (other.CompareTag(enemyTag))
        {
            // ... the enemy is no longer in range. Remove target
            anim.SetBool("IsAttacking", false);
            enemy = null;
            enemyHealth = null;
            enemyInRange = false;
        }
    }

    private void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the enemy has health to lose...
        if (enemyHealth.CurrentHealth() > 0)
        {
            anim.SetBool("IsMoving", false);
            anim.SetBool("IsAttacking", true);
            Vector3 attackDirection = (enemy.transform.position - transform.position).normalized;
            
            // Face the enemy
            transform.LookAt(enemy.transform);

            // ... damage the enemy.
            enemyHealth.TakeDamage(attackDamage, attackDirection);
        }
    }
}
