using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class RajaField : MonoBehaviour
    {
        public int damageAmount = 10; // Amount of damage to apply to the player
        public float damageCooldown = 2f; // Cooldown duration between damage applications

        public float speedToReduce = 0.2f;
        public float damageToReduce = 0.2f;

        private float lastDamageTime; // Timestamp of the last damage application

        private bool entered = false;
        PlayerHealth playerHealth;


        private void OnTriggerEnter(Collider other)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (other.CompareTag("Player"))
            {
                if (!entered)
                {

                    if (playerMovement != null)
                    {
                        playerMovement.AddBuff(-1f * speedToReduce);
                    }

                    if (playerHealth != null)
                    {
                        playerHealth.PowerUp(-1f * damageToReduce);
                    }
                }
                entered = true;

                if (Time.time - lastDamageTime >= damageCooldown)
                {
                    playerHealth.TakeDamage(damageAmount);
                    lastDamageTime = Time.time;
                    this.playerHealth = playerHealth;
                }
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.PowerUp(damageToReduce);
                }

                PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.AddBuff(speedToReduce);
                }
                entered = false;
            }
        }

        void Update()
        {
            if (Time.time - lastDamageTime < damageCooldown)
            {
                Debug.Log("Field Cooldown");
                return;
            }

            if (entered)
            {

                if (this.playerHealth != null)
                {
                    this.playerHealth.TakeDamage(damageAmount);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}
