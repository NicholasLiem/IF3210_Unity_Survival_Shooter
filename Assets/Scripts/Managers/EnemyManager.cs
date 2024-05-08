using UnityEngine;

namespace Nightmare
{
    public class EnemyManager : PausibleObject
    {
        private PlayerHealth playerHealth;
        public GameObject enemy;
        public float spawnTime = 3f;

        public float spawnQty = 9999;
        public Transform[] spawnPoints;

        private float timer;
        private int spawned = 0;

        void Start()
        {
            timer = spawnTime;
        }

        void OnEnable()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void Update()
        {
            if (isPaused)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0f && spawned < spawnQty)
            {
                Spawn();
                timer = spawnTime;
            }
        }

        void Spawn()
        {
            // If the player has no health left...
            if (playerHealth.currentHealth <= 0f || IsDead())
            {
                // ... exit the function.
                return;
            }

            if (spawnPoints.Length > 0)
            {
                // Find a random index between zero and one less than the number of spawn points.
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                spawned++;
            }
        }

        bool IsDead()
        {
            return !gameObject.activeSelf;
        }
    }
}
