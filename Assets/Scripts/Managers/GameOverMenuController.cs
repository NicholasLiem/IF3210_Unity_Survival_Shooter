using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{

    public void GoBackToMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        SceneManager.LoadScene(1);
    }
}
