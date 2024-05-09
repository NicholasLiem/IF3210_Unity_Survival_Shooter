using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        // Should we restart the stats?
        SceneManager.LoadScene("Level 01");
    }
}
