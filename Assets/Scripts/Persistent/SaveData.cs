using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public struct PlayerData
    {
        public string username;
        public float baseDamage;

        public GameDifficulty gameDifficulty;
    }

    [System.Serializable]
    public struct StatisticData
    {
        public float shotAccuracy;
        public int totalShotsFired;
        public int successfulHits;
        public float distanceTraveled;
        public int minutesPlayed;
        public int goldCollected;
        public Dictionary<string, int> orbsCollected;
        public Dictionary<string, int> enemyKillCount;
    }


    [System.Serializable]
    public struct QuestData
    {
        public int progress;
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
