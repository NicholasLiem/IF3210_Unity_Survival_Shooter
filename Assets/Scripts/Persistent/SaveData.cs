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
        // TODO: Add statistic data
    }

    [System.Serializable]
    public struct QuestData
    {
        // TODO: Add quest data
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