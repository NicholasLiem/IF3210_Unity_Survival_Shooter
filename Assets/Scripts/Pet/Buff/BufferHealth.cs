using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public float sinkSpeed = 5f;
    public int scoreValue = 10;
    public float buffPercentage = 0.2f;
    public EnemyAttack masterAttack;
    // public AudioClip deathClip;

    int currentHealth;
    // AudioSource enemyAudio;
    Animator anim;
    CapsuleCollider capsuleCollider;

    void Awake()
    {
        anim = GetComponent<Animator>();
        // enemyAudio = GetComponent<AudioSource>();
        
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void OnEnable()
    {
        currentHealth = startingHealth;
        SetKinematics(false);
    }

    void Start()
    {
        // Buff master damage
        masterAttack.IncreaseDamage(buffPercentage);
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

    public void TakeDamage(int amount)
    {
        if (!IsDead())
        {
            // enemyAudio.Play();
            currentHealth -= amount;

            if (IsDead())
            {
                if (masterAttack != null)
                {
                    masterAttack.DecreaseDamage(buffPercentage);
                }
                Death();
            }
        }
    }

    void Death()
    {
        // EventManager.TriggerEvent("Sound", this.transform.position);
        anim.SetTrigger("Dead");

        // enemyAudio.clip = deathClip;
        // enemyAudio.Play();
    }

    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        SetKinematics(true);
    }

    public int CurrentHealth()
    {
        return currentHealth;
    }
}
