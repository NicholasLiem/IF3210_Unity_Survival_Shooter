using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public float ShotAccuracy {get; private set;} = 0;
    public int TotalShotsFired { get; private set; } = 0;
    public int SuccessfulHits { get; private set; } = 0;
    public float DistanceTraveled { get; private set; } = 0;
    public int MinutesPlayed { get; private set; } = 0;
    public int GoldCollected {get; private set;} = 0;

    public Dictionary<string, int> OrbsCollected { get; private set; } = new Dictionary<string, int>();
    public Dictionary<string, int> EnemyKillCount { get; private set; } = new Dictionary<string, int>();

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.OnMinutePassed += UpdateMinutesPlayed;
        GameEventsManager.instance.miscEvents.OnOrbsCollected += AddOrbsCollected;
        GameEventsManager.instance.miscEvents.OnGoldCollected += AddGoldCollected;
        GameEventsManager.instance.enemyKilledEvents.OnEnemyKilled += AddEnemiesKilled;
        GameEventsManager.instance.playerActionEvents.OnShotFired += AddShotFired;
        GameEventsManager.instance.playerActionEvents.OnShotHit += AddSuccessfulHit;
    }

    public void AddDistance(float distance)
    {
        DistanceTraveled += distance;
    }


    private void UpdateMinutesPlayed(int minutes)
    {
        MinutesPlayed = minutes;
    }

    private void AddEnemiesKilled(string enemyType)
    {
        if (EnemyKillCount.ContainsKey(enemyType))
        {
            EnemyKillCount[enemyType]++;
        }
        else
        {
            EnemyKillCount.Add(enemyType, 1);
        }
    }

    private void AddOrbsCollected(string orbType)
    {
        if (OrbsCollected.ContainsKey(orbType))
        {
            OrbsCollected[orbType]++;
        }
        else
        {
            OrbsCollected.Add(orbType, 1);
        }
    }

    private void AddGoldCollected(int goldCollected)
    {
        GoldCollected += goldCollected;
    }

    private void AddShotFired()
    {
        TotalShotsFired++;
        UpdateShotAccuracy();
    }

    private void AddSuccessfulHit(bool successfulHit)
    {
        if (successfulHit)
        {
            SuccessfulHits++;
        }

        UpdateShotAccuracy();
    }

    private void UpdateShotAccuracy()
    {
        if (TotalShotsFired > 0)
        {
            ShotAccuracy = (float)SuccessfulHits / TotalShotsFired;
            Debug.Log("Shot Accuracy: " + ShotAccuracy);
            Debug.Log("Successful Hits: " + SuccessfulHits);
            Debug.Log("Total Hits: " + TotalShotsFired);
        }
    }
}
