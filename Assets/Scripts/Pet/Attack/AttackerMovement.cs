using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackerMovement : MonoBehaviour
{
    public float visionRange = 20f;

    // Gap distance with master
    public float distanceFromMaster = 1f;

    public GameObject masterObject;
    public float distanceFromMasterThreshold = 5f;

    public string enemyTag = "Enemy";

    Transform master;
    NavMeshAgent nav;
    Animator anim;
    bool isEnemyInRange;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        master = masterObject.transform;
    }

    void OnEnable()
    {
        nav.enabled = true;
    }

    void Update()
    {
        LookForPlayer();
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the enemy...
        if (other.CompareTag(enemyTag))
        {
            // ... the enemy is in range. Disable moving animation cos already in attack range
            anim.SetBool("IsMoving", false);
            isEnemyInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the enemy...
        if (other.CompareTag(enemyTag))
        {
            isEnemyInRange = false;
        }
    }

    private void TestSense(Vector3 position, float senseRange)
    {
        if (Vector3.Distance(this.transform.position, position) >= senseRange)
        {
            GoToPosition(position);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    private void GoToPosition(Vector3 position)
    {
        anim.SetBool("IsMoving", true);
        SetDestination(position);
    }

    private void SetDestination(Vector3 position)
    {
        if (nav.isOnNavMesh)
        {
            // Calc the direction from the player position to the NavMeshAgent
            Vector3 direction = (transform.position - position).normalized;

            // Calc the new position with space
            Vector3 newPosition = position + direction * distanceFromMaster;

            nav.SetDestination(newPosition);
        }
    }

    private void LookForPlayer()
    {
        // Scan for closest enemy
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            if (isEnemyInRange) // Disable moving cos already in attack range
            {
                return;
            }
            // Move closer to the enemy
            GoToPosition(closestEnemy.position);
        }
        else
        {
            // Follow the master
            TestSense(master.position, distanceFromMasterThreshold);
        }
    }
}
