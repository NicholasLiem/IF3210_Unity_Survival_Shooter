using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BufferMovement : MonoBehaviour
{
    public string playerTag = "Player";
    // Gap distance with master
    public float distanceFromMaster = 1.0f;
    public GameObject masterObject;

    Transform master, player;
    NavMeshAgent nav;
    Animator anim;
    bool playerInRange;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Start()
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
        // If the entering collider is the player...
        if (other.CompareTag(playerTag))
        {
            // ... the player is in range. Avoid player
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
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
            Vector3 newPosition;

            if (playerInRange)
            {
                // Avoid player
                newPosition = transform.position + direction;
            } 
            else
            {
                // Follow master
                newPosition = position + direction * distanceFromMaster;
            }

            nav.SetDestination(newPosition);
        }
    }

    private void LookForPlayer()
    {
        if (playerInRange)
        {
            GoToPosition(player.position);
            return;
        }
        if (masterObject != null)
        {
            TestSense(master.position, distanceFromMaster);
        } else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}
