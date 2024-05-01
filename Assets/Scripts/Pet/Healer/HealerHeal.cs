using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAttack : MonoBehaviour
{
    public float timeBetweenHeals = 2f;
    public int healAmount = 10;

    float timer;
    Animator anim;
    PlayerHealth playerHealth;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between heals, check if player health is decreased and not dead...
        if (timer >= timeBetweenHeals)
        {
            if (playerHealth.currentHealth > 0 && playerHealth.currentHealth != playerHealth.startingHealth)
            {
                // Heals only when player health is not max
                Heal();
            }
        }
        
        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            // Kill the pet
            anim.SetTrigger("Dead");
        }
    }

    private void Heal()
    {
        // Reset the timer.
        timer = 0f;

        anim.SetBool("IsMoving", false);
        anim.SetBool("IsHealing", true);
        playerHealth.Heal(healAmount);
    }
}
