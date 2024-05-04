using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

public class JenderalPet : MonoBehaviour
{
    public GameObject petPrefab;

    private void Awake()
    {
        int numOfPets = 1;
        for (int i = 0; i < numOfPets; i++)
        {
            // Instantiate the prefab
            GameObject instance = Instantiate(petPrefab, transform.position, Quaternion.identity);

            // Get the script component
            BufferMovement moveScript = instance.GetComponent<BufferMovement>();
            BufferHealth healthScript = instance.GetComponent<BufferHealth>();

            // Set the attribute
            moveScript.masterObject = this.gameObject;
            healthScript.masterAttack = this.gameObject.GetComponent<EnemyAttack>();
        }
    }
}
