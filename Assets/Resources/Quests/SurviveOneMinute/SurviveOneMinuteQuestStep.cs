using UnityEngine;

public class SurviveOneMinuteQuestStep : QuestStep
{
    private int minutesToComplete = 1;
    private int totalMinutes = 0;

    private void OnEnable()
    {
        GameEventsManager.Instance.miscEvents.OnMinutePassed += OneMinuteHasPassed;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.miscEvents.OnMinutePassed -= OneMinuteHasPassed;
    }

    private void OneMinuteHasPassed()
    {
        totalMinutes++;
        if (totalMinutes >= minutesToComplete)
        {
            FinishQuestStep();
        }
    }
}
