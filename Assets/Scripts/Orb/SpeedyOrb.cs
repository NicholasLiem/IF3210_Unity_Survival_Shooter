using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class SpeedyOrb : MonoBehaviour
    {
        public string orbType = "Speedy Orb";
        public float speedToAdd = 0.2f;
        public float duration = 15f;

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
                GameEventsManager.Instance.miscEvents.TriggerOrbsCollected(orbType);

                PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

                if (playerMovement != null)
                {
                    playerMovement.Buff(speedToAdd, duration);
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
