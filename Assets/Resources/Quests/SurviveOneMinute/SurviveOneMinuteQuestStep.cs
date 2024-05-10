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
        UpdateState();
        if (totalMinutes >= minutesToComplete)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = totalMinutes.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.minutesToComplete = System.Int32.Parse(state);
        UpdateState();
    }
}
