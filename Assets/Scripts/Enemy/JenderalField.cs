using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class JenderalField : MonoBehaviour
    {
        public int damageAmount = 10; // Amount of damage to apply to the player
        public float damageCooldown = 2f; // Cooldown duration between damage applications

        private float lastDamageTime = 0f; // Timestamp of the last damage application

        private bool isInside = false;
        PlayerHealth playerHealth;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && Time.time - lastDamageTime >= damageCooldown)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    this.playerHealth = playerHealth;
                    lastDamageTime = Time.time;
                    isInside = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isInside = false;
            }
        }

        void Update()
        {
            if (Time.time - lastDamageTime < damageCooldown)
            {
                Debug.Log("Field Cooldown");
                return;
            }

            if (isInside)
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
