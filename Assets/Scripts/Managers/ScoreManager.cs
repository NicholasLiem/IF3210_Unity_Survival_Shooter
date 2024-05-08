using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Nightmare
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;
        public static int gold = 0;
        private int levelThreshhold;
        const int LEVEL_INCREASE = 300;
        public static bool motherlodeCheat = false;

        Text sText;

        void Awake()
        {
            GameObject scoreTextObject = GameObject.Find("ScoreText");
            if (scoreTextObject != null)
                sText = scoreTextObject.GetComponent<Text>();
            score = 0;
            levelThreshhold = LEVEL_INCREASE;
        }

        public static void MotherlodeCheat()
        {
            motherlodeCheat = true;
        }


        void Update()
        {
            sText.text = "Score: " + score;
            // if (score >= levelThreshhold)
            // {
            //     AdvanceLevel();
            // }
        }

        private void AdvanceLevel()
        {
            levelThreshhold = score + LEVEL_INCREASE;
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.AdvanceLevel();
        }
    }
}
