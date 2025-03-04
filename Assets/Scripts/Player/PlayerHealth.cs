﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;

        public float maxDamage = 2.5f;
        public float baseDamage;

        public Slider healthSlider;
        public Image damageImage;
        public AudioClip deathClip;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
        public bool godMode = false;

        Animator anim;
        AudioSource playerAudio;
        PlayerMovement playerMovement;
        PlayerShooting playerShooting;
        public bool isDead;
        bool damaged;
        bool healed;
        bool noDamageCheat = false;

        void Awake()
        {
            // Setting up the references.
            anim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovement>();
            playerShooting = GetComponentInChildren<PlayerShooting>();

            ResetPlayer();
        }

        private void Start()
        {
            baseDamage = GameManager.Instance.baseDemage;
        }

        public void NoDamageCheat()
        {
            noDamageCheat = true;
        }

        public void ResetPlayer()
        {
            // Set the initial health of the player.
            currentHealth = startingHealth;

            playerMovement.enabled = true;
            playerShooting.enabled = true;

            anim.SetBool("IsDead", false);
        }


        void Update()
        {
            // If the player has just been damaged...
            if (damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            else if (healed)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = Color.green;
                healed = false;

            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;

        }


        public void TakeDamage(int amount)
        {
            if (isDead)
            {
                return;
            }
            Debug.Log(noDamageCheat);
            if (!noDamageCheat)
            {
                if (godMode)
                    return;

                // Set the damaged flag so the screen will flash.
                damaged = true;

                // Reduce the current health by the damage amount.
                currentHealth -= amount;

                // Set the health bar's value to the current health.
                healthSlider.value = currentHealth;

                // Play the hurt sound effect.
                playerAudio.Play();

                // If the player has lost all it's health and the death flag hasn't been set yet...
                if (currentHealth <= 0 && !isDead)
                {
                    // ... it should die.
                    GameManager.Instance.petData.Clear();
                    Death();
                }
            }
            Debug.Log("This is current health " + currentHealth);
        }

        public void Heal(float amount)
        {
            healed = true;
            // cast to int
            currentHealth += (int)(currentHealth * amount);

            if (currentHealth > startingHealth)
            {
                currentHealth = startingHealth;
            }

            healthSlider.value = currentHealth;
        }

        void Death()
        {
            // Set the death flag so this function won't be called again.
            isDead = true;

            // // Turn off any remaining shooting effects.
            playerShooting.disable();

            // Tell the animator that the player is dead.
            anim.SetBool("IsDead", true);

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            playerAudio.clip = deathClip;
            playerAudio.Play();

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }

        public void RestartLevel()
        {
            EventManager.TriggerEvent("GameOver");
        }

        public void PowerUp(float amount)
        {

            baseDamage += amount;
            if (baseDamage > maxDamage)
            {
                baseDamage = maxDamage;
            }
            GameManager.Instance.baseDemage = baseDamage;
            // get playershooting script
            playerShooting = GetComponentInChildren<PlayerShooting>();
            playerShooting.setMultiplier(baseDamage);
        }
    }
}