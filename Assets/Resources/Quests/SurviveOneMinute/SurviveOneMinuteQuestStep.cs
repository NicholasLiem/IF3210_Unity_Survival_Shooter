using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveOneMinuteQuestStep : QuestStep
{
    private float secondsPlayed = 0f;
    private int minutesPlayed = 0;
    private int minutesToComplete = 1;

    private void Update()
    {
        secondsPlayed += Time.deltaTime;
        if (secondsPlayed >= 60)  // Each minute has 60 seconds
        {
            secondsPlayed -= 60;  // Reset the seconds counter after each minute
            minutesPlayed++;  // Increment the minutes counter

            if (minutesPlayed >= minutesToComplete)
            {
                FinishQuestStep();
            }
        }
    }

    // Optionally, for debugging purposes, you might want to see the current time progression
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Minutes Played: " + minutesPlayed);
    }
}
