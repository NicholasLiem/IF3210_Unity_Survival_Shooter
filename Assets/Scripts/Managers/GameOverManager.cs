using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public float restartDelay = 5f;
        Animator anim;
        float restartTimer;

        LevelManager lm;
        private UnityEvent listener;

        void Awake ()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            anim = GetComponent <Animator> ();
            lm = FindObjectOfType<LevelManager>();
            EventManager.StartListening("GameOver", ShowGameOver);
        }

        void Update()
        {
            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("GameOver");
                restartTimer += Time.deltaTime;
                if (restartTimer >= restartDelay)
                {
                    Application.LoadLevel(Application.loadedLevel);
                    LoadMainMenu();
                }
            }
        }

        void OnDestroy()
        {
            EventManager.StopListening("GameOver", ShowGameOver);
        }

        void ShowGameOver()
        {
            anim.SetBool("GameOver", true);
        }

        private void ResetLevel()
        {
            // Reset stats when game is finished
            GameManager.Instance.PlayerStats.ResetStats();
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.LoadInitialLevel();
            anim.SetBool("GameOver", false);
            playerHealth.ResetPlayer();
        }

        private void LoadMainMenu()
        {
            GameManager.Instance.currentLevel = 1;
            SceneManager.LoadScene("Main Menu");
        }
    }
}
