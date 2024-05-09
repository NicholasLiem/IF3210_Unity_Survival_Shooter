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
        Debug.Log("Enemy type killed: " + enemyType);
        if (enemyType == "Keroco")
        {
            kerocoCount++;
            Debug.Log($"Keroco killed. Total: {kerocoCount}");
        }
        else if (enemyType == "KepalaKeroco")
        {
            kepalaKerocoCount++;
            Debug.Log($"KepalaKeroco killed. Total: {kepalaKerocoCount}");
        }

        if (kerocoCount >= requiredKerocoCount && kepalaKerocoCount >= requiredKepalaKerocoCount)
        {
            Debug.Log("Quest step complete: Killed sufficient Kerocos and KepalaKerocos");
            CompleteQuestStep();
        }
    }

    private void CompleteQuestStep()
    {
        Debug.Log("Completing the LimaKerocoDuaKepala quest step.");
        FinishQuestStep();
    }
}
