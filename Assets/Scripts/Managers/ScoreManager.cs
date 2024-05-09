using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Nightmare
{
    public class ScoreManager : MonoBehaviour
    {
        private int levelThreshhold;
        const int LEVEL_INCREASE = 300;
        public static bool motherlodeCheat = false;


        void Awake()
        {
            levelThreshhold = LEVEL_INCREASE;
        }

        public static void MotherlodeCheat()
        {
            motherlodeCheat = true;
        }


        void Update()
        {
            // if (score >= levelThreshhold)
            // {
            //     AdvanceLevel();
            // }
        }

        private void AdvanceLevel()
        {
            levelThreshhold = GameManager.Instance.PlayerStats.Score + LEVEL_INCREASE;
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.AdvanceLevel();
        }
    }
}
