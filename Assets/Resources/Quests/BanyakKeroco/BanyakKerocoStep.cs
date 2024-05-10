using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;

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

        UpdateState();
        CheckQuestCompletion();
    }

    private void OneMinuteHasPassed()
    {
        totalMinutes++;
        UpdateState();

        if (totalMinutes >= minutesToComplete)
        {
            if (!CheckQuestCompletion())
            {
                KillPlayer();
            }
        }
    }

    private void UpdateState()
    {
        string state = $"{kerocoCount},{kepalaKerocoCount},{jenderalCount},{totalMinutes}";
        ChangeState(state);
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

    protected override void SetQuestStepState(string state)
    {
        var states = state.Split(',');
        if (states.Length == 4)
        {
            kerocoCount = int.Parse(states[0]);
            kepalaKerocoCount = int.Parse(states[1]);
            jenderalCount = int.Parse(states[2]);
            totalMinutes = int.Parse(states[3]);
        }

        CheckQuestCompletion();
        if (totalMinutes >= minutesToComplete)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null && !playerHealth.isDead)
        {
            playerHealth.TakeDamage(playerHealth.currentHealth);
        }
        else
        {
            Debug.LogError("PlayerHealth component not found in the scene.");
        }
    }

}
