using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public struct PetData
    {
        public string name;
        public int count;
    }

    [System.Serializable]
    public struct PlayerData
    {
        public string username;
        public float baseDamage;
        
        public PetData[] petData;

        public GameDifficulty gameDifficulty;
    }

    [System.Serializable]
    public struct StatisticData
    {
        public float shotAccuracy;
        public int totalShotsFired;
        public int successfillHits;
        public float distanceTraveled;
        public int goldCollected;
        
        public int score;

        public int secondsPlayed;

        public Dictionary<string, int> orbsCollected;
        public Dictionary<string, int> enemyKillCount;
    }


    [System.Serializable]
    public struct QuestData
    {
        public int progress;
        public int currentLevel;
    }

    public PlayerData playerData;
    public StatisticData statisticData;
    public QuestData questData;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}
