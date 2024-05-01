using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RajaPet : MonoBehaviour
{
    public GameObject petPrefab;

    private void Awake()
    {
        int numOfPets = 2;
        for (int i = 0; i < numOfPets; i++)
        {
            // Instantiate the prefab
            GameObject instance = Instantiate(petPrefab, transform.position, Quaternion.identity);

            // Get the script component
            BufferMovement moveScript = instance.GetComponent<BufferMovement>();
            BufferHealth healthScript = instance.GetComponent<BufferHealth>();

            // Set the attribute
            moveScript.masterObject = this.gameObject;
            healthScript.masterAttack = this.gameObject.GetComponent<EnemyAttack>(); // Question: is this passed by value?
        }
    }
}
