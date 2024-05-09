using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunuhRajaStep : QuestStep
{
    private int rajaCount = 0;
    private int requiredRajaCount = 1;

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
        if (enemyType == "Raja")
        {
            rajaCount++;
        }

        if (rajaCount >= requiredRajaCount)
        {
            CompleteQuestStep();
        }
    }

    private void CompleteQuestStep()
    {
        FinishQuestStep();
    }

}
