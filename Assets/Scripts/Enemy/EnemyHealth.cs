using UnityEngine;

namespace Nightmare
{
    public class EnemyHealth : MonoBehaviour
    {
        public GameObject[] orbPrefabs;
        public int startingHealth = 100;
        public float sinkSpeed = 5f;
        public float spawnOrbChance = 0.5f;
        public int scoreValue = 10;
        public int goldValue = 5;
        public AudioClip deathClip;
        public EnemyData data;
        int currentHealth;
        Animator anim;
        AudioSource enemyAudio;
        ParticleSystem hitParticles;
        ParticleSystem deathParticles;
        CapsuleCollider capsuleCollider;
        EnemyMovement enemyMovement;

        public ParticleSystem angryParticles;

        void Awake()
        {
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            enemyMovement = this.GetComponent<EnemyMovement>();

        }

        void OnEnable()
        {
            currentHealth = startingHealth;
            SetKinematics(false);
        }

        private void SetKinematics(bool isKinematic)
        {
            capsuleCollider.isTrigger = isKinematic;
            capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        }

        void Update()
        {
            if (IsDead())
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
                if (transform.position.y < -5f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        public bool IsDead()
        {
            return (currentHealth <= 0f);
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (!IsDead())
            {
                enemyAudio.Play();
                currentHealth -= amount;

                // not is playing angryparticles
                if (angryParticles != null && !angryParticles.isPlaying && currentHealth <= 0.6 * startingHealth)
                {
                    triggerAngry();
                }

                if (IsDead())
                {
                    Death();
                }
                else
                {
                    enemyMovement.GoToPlayer();
                }
            }

            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
        }

        void Death()
        {
            EventManager.TriggerEvent("Sound", this.transform.position);
            anim.SetTrigger("Dead");

            enemyAudio.clip = deathClip;
            enemyAudio.Play();
            if (Random.value < spawnOrbChance)
            {
                SpawnOrb();
            }

            GameEventsManager.instance.enemyKilledEvents.TriggerEnemyKilled(data.enemyType);
        }

        private void SpawnOrb()
        {
            if (orbPrefabs.Length > 0)
            {
                GameObject selectedOrbPrefab = orbPrefabs[Random.Range(0, orbPrefabs.Length)];
                Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;
                Instantiate(selectedOrbPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No orb prefabs assigned!");
            }
        }

        public void StartSinking()
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            SetKinematics(true);

            ScoreManager.score += scoreValue;
            ScoreManager.gold += goldValue;
            // TODO: Change gold into game manager instead of score manager
            GameEventsManager.instance.miscEvents.TriggerGoldCollected(goldValue);
        }

        public int CurrentHealth()
        {
            return currentHealth;
        }

        private void triggerAngry()
        {
            angryParticles.Play();

            EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
            enemyMovement.angry();
        }
    }
}
