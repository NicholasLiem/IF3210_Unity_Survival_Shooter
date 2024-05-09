using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public float ShotAccuracy {get; private set;} = 0;
    public int TotalShotsFired { get; private set; } = 0;
    public int SuccessfulHits { get; private set; } = 0;
    public float DistanceTraveled { get; private set; } = 0;
    public int MinutesPlayed { get; private set; } = 0;
    public int GoldCollected {get; private set;} = 0;

    public Dictionary<string, int> OrbsCollected { get; private set; } = new Dictionary<string, int>();
    public Dictionary<string, int> EnemyKillCount { get; private set; } = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (GameEventsManager.Instance != null)
        {
            GameEventsManager.Instance.miscEvents.OnMinutePassed += UpdateMinutesPlayed;
            GameEventsManager.Instance.miscEvents.OnOrbsCollected += AddOrbsCollected;
            GameEventsManager.Instance.miscEvents.OnGoldCollected += AddGoldCollected;
            GameEventsManager.Instance.enemyKilledEvents.OnEnemyKilled += AddEnemiesKilled;
            GameEventsManager.Instance.playerActionEvents.OnShotFired += AddShotFired;
            GameEventsManager.Instance.playerActionEvents.OnShotHit += AddSuccessfulHit;
            GameEventsManager.Instance.playerActionEvents.OnPlayerMovement += AddDistance;
        }
    }

    private void AddDistance(float distance)
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
        }
    }

    private void ResetStats()
    {
        ShotAccuracy = 0;
        TotalShotsFired = 0;
        SuccessfulHits = 0;
        DistanceTraveled = 0;
        MinutesPlayed = 0;
        GoldCollected = 0;
        OrbsCollected = new Dictionary<string, int>();
        EnemyKillCount = new Dictionary<string, int>();
    }
}
