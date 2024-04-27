using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class JenderalField : MonoBehaviour
    {
        public int damageAmount = 10; // Amount of damage to apply to the player
        public float damageCooldown = 2f; // Cooldown duration between damage applications

        private float lastDamageTime; // Timestamp of the last damage application

        private void OnTriggerEnter(Collider other)
        {
            // Check if the collider is the player and if enough time has passed since the last damage application
            if (other.CompareTag("Player") && Time.time - lastDamageTime >= damageCooldown)
            {
                // Get the PlayerHealth component from the player GameObject
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

                // If the PlayerHealth component is found, apply damage to the player
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);

                    // Update the timestamp of the last damage application
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}
