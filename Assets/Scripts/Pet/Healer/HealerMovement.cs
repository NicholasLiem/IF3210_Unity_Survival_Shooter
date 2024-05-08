using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealerMovement : MonoBehaviour
{
    // Gap distance with master
    public float distanceFromMaster = 2.0f;
    GameObject masterObject;

    Transform master;
    NavMeshAgent nav;
    Animator anim;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        masterObject = GameObject.FindGameObjectWithTag("Player");
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
        TestSense(master.position, distanceFromMaster);
    }
}
