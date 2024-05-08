using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Nightmare
{
    public class TimerManager : MonoBehaviour
    {
        public float totalDuration = 120f;

        private float currentDuration;

        Text sText;

        void Awake()
        {
            GameObject scoreTextObject = GameObject.Find("TimerText");
            if (scoreTextObject != null)
                sText = scoreTextObject.GetComponent<Text>();
            currentDuration = totalDuration;
        }


        void Update()
        {
            int seconds = (int)currentDuration;
            sText.text = "Timer: " + seconds;
            if (currentDuration <= 0)
            {
                AdvanceLevel();
            }
            else
            {
                // minus by time delta
                currentDuration -= Time.deltaTime;
            }
        }

        // On final scene, boss will call lm.advancelevel().
        // See EnemyHealth.cs
        private void AdvanceLevel()
        {
            GameManager.Instance.AdvanceLevel(true);
        }
    }
}
