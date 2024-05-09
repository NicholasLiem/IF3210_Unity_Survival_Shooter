using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class HealingOrb : MonoBehaviour
    {
        public string orbType = "Healing Orb";
        public float despawnDelay = 5f; // Delay before despawning the orb

        private void Start()
        {
            // Start the despawn timer when the orb is instantiated
            Invoke("DespawnOrb", despawnDelay);
        }
        public float healthToAdd = 0.20f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("PICKED UP");
                GameEventsManager.Instance.miscEvents.TriggerOrbsCollected(orbType);

                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.Heal(healthToAdd);
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
