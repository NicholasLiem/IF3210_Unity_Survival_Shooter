using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{

    public void GoBackToMainMenu()
    {
        GameManager.Instance.PlayerStats.ResetStats();
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        // Resetting the stats
        GameManager.Instance.PlayerStats.ResetStats();
        SceneManager.LoadScene("Level 01");
    }
}
