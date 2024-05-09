using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanyakKerocoStep : QuestStep
{
    private int kerocoCount = 0;
    private int kepalaKerocoCount = 0;
    private int jenderalCount = 0;

    private int requiredKerocoCount = 10;
    private int requiredKepalaKerocoCount = 5;
    private int requiredJenderalCount = 1;

    private int totalMinutes = 0;
    private int minutesToComplete = 2;

    private void OnEnable()
    {
        GameEventsManager.Instance.enemyKilledEvents.OnEnemyKilled += OnEnemyKilled;
        GameEventsManager.Instance.miscEvents.OnMinutePassed += OneMinuteHasPassed;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.enemyKilledEvents.OnEnemyKilled -= OnEnemyKilled;
        GameEventsManager.Instance.miscEvents.OnMinutePassed -= OneMinuteHasPassed;
    }

    private void OnEnemyKilled(string enemyType)
    {
        switch (enemyType)
        {
            case "Keroco":
                kerocoCount++;
                break;
            case "KepalaKeroco":
                kepalaKerocoCount++;
                break;
            case "Jenderal":
                jenderalCount++;
                break;
        }

        CheckQuestCompletion();
    }

    private void OneMinuteHasPassed()
    {
        totalMinutes++;
        Debug.Log("Minutes passed: " + totalMinutes);

        if (totalMinutes >= minutesToComplete)
        {
            if (!CheckQuestCompletion())
            {
                KillPlayer();
            }
        }
    }

    private bool CheckQuestCompletion()
    {
        if (kerocoCount >= requiredKerocoCount && kepalaKerocoCount >= requiredKepalaKerocoCount && jenderalCount >= requiredJenderalCount)
        {
            Debug.Log("Quest step completed successfully.");
            FinishQuestStep();
            return true;
        }

        return false;
    }

    private void KillPlayer()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null && !playerHealth.isDead)
        {
            playerHealth.TakeDamage(playerHealth.currentHealth);
        }
        else if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found in the scene.");
        }
    }

}
