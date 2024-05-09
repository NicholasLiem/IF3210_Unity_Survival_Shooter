using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Nightmare
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public float gameOverDelay = 3f;

        void Awake()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }

        void Update()
        {
            if (playerHealth.currentHealth <= 0)
            {
                StartCoroutine(DelayedLoadGameOver());
            }
        }

        IEnumerator DelayedLoadGameOver()
        {
            yield return new WaitForSeconds(gameOverDelay);
            ResetGame();
            SceneManager.LoadScene("Game Over");
        }

        private void ResetGame()
        {
            GameManager.Instance.currentLevel = 1;
            playerHealth.ResetPlayer();
        }
    }
}
