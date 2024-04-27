using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class PowerOrb : MonoBehaviour
    {
        public float powerToAdd = 0.10f;

        public float despawnDelay = 5f; // Delay before despawning the orb

        private void Start()
        {
            // Start the despawn timer when the orb is instantiated
            Invoke("DespawnOrb", despawnDelay);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("PICKED UP");

                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.PowerUp(powerToAdd);
                }

                CancelInvoke("DespawnOrb");
                Destroy(gameObject);
            }
        }

        private void DespawnOrb()
        {
            Debug.Log("Orb despawned due to timeout");
            Destroy(gameObject);
        }
    }

}