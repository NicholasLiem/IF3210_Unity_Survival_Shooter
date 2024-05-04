using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public float ShotAccuracy {get; private set;}
    public float DistanceTraveled { get; private set; }
    public int MinutesPlayed { get; private set; } 
    public int OrbsCollected {get; private set;}
    public int GoldCollected {get; private set;}

    public Dictionary<string, int> EnemyKillCount { get; private set; } = new Dictionary<string, int>();


    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.OnMinutePassed += UpdateMinutesPlayed;
        GameEventsManager.instance.enemyKilledEvents.OnEnemyKilled += AddEnemiesKilled;
    }

    private void UpdateMinutesPlayed(int minutes)
    {
        MinutesPlayed = minutes;
    }

    public void AddDistance(float distance)
    {
        DistanceTraveled += distance;
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

    private void AddOrbsCollected(int orbsCollected)
    {
        OrbsCollected += orbsCollected;
    }

    private void AddGoldCollected(int goldCollected)
    {
        GoldCollected += goldCollected;
    }

    private void UpdateShotAccuracy()
    {
        return;
    }
}
