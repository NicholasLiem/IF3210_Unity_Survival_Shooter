using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimaKerocoDuaKepalaStep : QuestStep
{
    private int kerocoCount = 0;
    private int kepalaKerocoCount = 0;
    private int requiredKerocoCount = 5;
    private int requiredKepalaKerocoCount = 2;

    private void OnEnable()
    {
        GameEventsManager.Instance.enemyKilledEvents.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.enemyKilledEvents.OnEnemyKilled -= OnEnemyKilled;
    }

    private void OnEnemyKilled(string enemyType)
    {
        if (enemyType == "Keroco")
        {
            kerocoCount++;
        }
        else if (enemyType == "KepalaKeroco")
        {
            kepalaKerocoCount++;
        }

        UpdateState();
        CheckCompletion();
    }

    private void UpdateState()
    {
        string state = $"{kerocoCount},{kepalaKerocoCount}";
        ChangeState(state);
    }

    private void CheckCompletion()
    {
        if (kerocoCount >= requiredKerocoCount && kepalaKerocoCount >= requiredKepalaKerocoCount)
        {
            CompleteQuestStep();
        }
    }

    private void CompleteQuestStep()
    {
        FinishQuestStep();
    }

    protected override void SetQuestStepState(string state)
    {
        var states = state.Split(',');
        if (states.Length == 2)
        {
            kerocoCount = int.Parse(states[0]);
            kepalaKerocoCount = int.Parse(states[1]);
        }
        UpdateState();
        CheckCompletion();
    }
}
