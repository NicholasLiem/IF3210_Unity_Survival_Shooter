using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameDifficulty : ushort
{
    Easy = 0,
    Medium = 1,
    Hard = 2
}

public class GameManager : MonoBehaviour, ISaveable
{
    public static GameManager Instance;

    public string Username { get; set; } = "";
    public GameDifficulty gameDifficulty = GameDifficulty.Easy;
    public List<Tuple<string, int>> petData = new List<Tuple<string, int>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    /*
     * Save and Load
     */

    public void PopulateSaveData(SaveData saveData)
    {
        SaveData.PlayerData playerData = new();

        playerData.username = this.Username;
        playerData.gameDifficulty = this.gameDifficulty;

        saveData.playerData = playerData;
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        this.Username = saveData.playerData.username;
        this.gameDifficulty = saveData.playerData.gameDifficulty;
    }

    public void SaveGame(int num)
    {
        SaveData sd = new();
        PopulateSaveData(sd);

        Debug.Log(sd);
        if (FileManager.WriteToFile(num + ".dat", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }

    public void LoadGame(int num)
    {
        if (FileManager.LoadFromFile(num + ".dat", out var json))
        {
            SaveData sd = new();
            sd.LoadFromJson(json);

            LoadFromSaveData(sd);
            Debug.Log("Load complete");
        }
    }

    public int GetPetAmount(string petName)
    {
        if (petData == null || !petData.Any())
        {
            return 0;
        }

        var pet = petData.FirstOrDefault(t => t.Item1 == petName);
        if (pet == null)
        {
            return 0;
        }

        return pet.Item2;
    }

}
