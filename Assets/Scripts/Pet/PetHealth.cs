using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public float sinkSpeed = 5f;
    public bool godMode = false;
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
        if (godMode)
        {
            return;
        }

        if (!IsDead())
        {
            // enemyAudio.Play();
            currentHealth -= amount;

            if (IsDead())
            {
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

    public void enableGodMode()
    {
        godMode = true;
    }

    public void disableGodMode()
    {
        godMode = false;
    }

    public void instantKillPet()
    {
        currentHealth = 0;
    }
}
