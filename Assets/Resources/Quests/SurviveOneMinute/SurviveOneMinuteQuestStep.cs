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
        Debug.Log("Minute passssssed");
        totalMinutes++;
        if (totalMinutes >= minutesToComplete)
        {
            Debug.Log("Finish quest step?");
            FinishQuestStep();
        }
    }
}
