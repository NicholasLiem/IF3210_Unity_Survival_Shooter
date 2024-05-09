using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour, ISaveable
{
    private bool eventsSubscribed = false;
    public static PlayerStats Instance { get; private set; }
    public float ShotAccuracy {get; private set;} = 0;
    public int TotalShotsFired { get; private set; } = 0;
    public int SuccessfulHits { get; private set; } = 0;
    public float DistanceTraveled { get; private set; } = 0;
    public int SecondsPlayed { get; private set; } = 0;
    public int GoldCollected {get; private set;} = 0;
    public int Score {get; private set;} = 0;

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
        GameEventsManager.Ready += SubscribeToEvents;
    }

    private void OnDisable()
    {
        GameEventsManager.Ready -= SubscribeToEvents;
    }

    private void SubscribeToEvents()
    {
        if (GameEventsManager.Instance != null)
        {
            GameEventsManager.Instance.miscEvents.OnSecondPassed += AddSecondsPlayed;
            GameEventsManager.Instance.miscEvents.OnOrbsCollected += AddOrbsCollected;
            GameEventsManager.Instance.miscEvents.OnGoldCollected += AddGoldCollected;
            GameEventsManager.Instance.enemyKilledEvents.OnEnemyKilled += AddEnemiesKilled;
            GameEventsManager.Instance.playerActionEvents.OnShotFired += AddShotFired;
            GameEventsManager.Instance.playerActionEvents.OnShotHit += AddSuccessfulHit;
            GameEventsManager.Instance.playerActionEvents.OnPlayerMovement += AddDistance;
        }
    }

    private void Update()
    {
        if (!eventsSubscribed && GameEventsManager.Instance != null)
        {
            SubscribeToEvents();
            eventsSubscribed = true;
        }
    }

    private void AddDistance(float distance)
    {
        DistanceTraveled += distance / 1000.0f;
    }

    private void AddSecondsPlayed()
    {
        SecondsPlayed++;
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

    public void DeductGoldCollected(int price)
    {
        if (price > GoldCollected)
        {
            return;
        }
        GoldCollected -= price;
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

    public void AddScore(int score)
    {
        Debug.Log("Score added: " + score);
        Score += score;
    }

    public void ResetStats()
    {
        ShotAccuracy = 0;
        TotalShotsFired = 0;
        SuccessfulHits = 0;
        DistanceTraveled = 0;
        SecondsPlayed = 0;
        GoldCollected = 0;
        Score = 0;
        OrbsCollected = new Dictionary<string, int>();
        EnemyKillCount = new Dictionary<string, int>();
    }

    public void PopulateSaveData(SaveData saveData)
    {
        SaveData.StatisticData statisticData = new();

        statisticData.shotAccuracy = this.ShotAccuracy;
        statisticData.totalShotsFired = this.TotalShotsFired;
        statisticData.successfillHits = this.SuccessfulHits;
        statisticData.distanceTraveled = this.DistanceTraveled;
        statisticData.secondsPlayed = this.SecondsPlayed;
        statisticData.goldCollected = this.GoldCollected;
        statisticData.orbsCollected = this.OrbsCollected;
        statisticData.enemyKillCount = this.EnemyKillCount;
        statisticData.score = this.Score;

        saveData.statisticData = statisticData;
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        this.ShotAccuracy = saveData.statisticData.shotAccuracy;
        this.TotalShotsFired = saveData.statisticData.totalShotsFired;
        this.SuccessfulHits = saveData.statisticData.successfillHits;
        this.DistanceTraveled = saveData.statisticData.distanceTraveled;
        this.SecondsPlayed = saveData.statisticData.secondsPlayed;
        this.GoldCollected = saveData.statisticData.goldCollected;
        this.OrbsCollected = saveData.statisticData.orbsCollected;
        this.EnemyKillCount = saveData.statisticData.enemyKillCount;
        this.Score = saveData.statisticData.score;
    }

    public string GetFormattedTimePlayed()
    {
        TimeSpan time = TimeSpan.FromSeconds(SecondsPlayed);
        return time.ToString(@"hh\:mm\:ss");
    }
}
