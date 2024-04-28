using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveOneMinuteQuestStep : QuestStep
{
    private int minutesToComplete = 1;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.OnMinutePassed += OneMinuteHasPassed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.OnMinutePassed -= OneMinuteHasPassed;
    }

    private void OneMinuteHasPassed(int totalMinutes)
    {
        Debug.Log("Total Minutes: " + totalMinutes);
        if (totalMinutes >= minutesToComplete)
        {
            FinishQuestStep();
        }
    }
}
