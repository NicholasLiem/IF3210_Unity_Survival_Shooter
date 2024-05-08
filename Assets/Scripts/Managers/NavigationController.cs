using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{

    float timeLeft = 10.0f;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            switch (GameManager.Instance.questProgress) {
                case 0:
                    SceneManager.LoadScene(1);
                    break;
                default:
                    break;
            }
        }
    }
}
