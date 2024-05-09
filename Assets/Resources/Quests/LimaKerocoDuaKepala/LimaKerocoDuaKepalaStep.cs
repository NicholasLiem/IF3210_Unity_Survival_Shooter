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

        if (kerocoCount >= requiredKerocoCount && kepalaKerocoCount >= requiredKepalaKerocoCount)
        {
            CompleteQuestStep();
        }
    }

    private void CompleteQuestStep()
    {
        FinishQuestStep();
    }
}
