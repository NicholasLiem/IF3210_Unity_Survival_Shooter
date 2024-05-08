using System;
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
    public float baseDemage = 1f;

    public GameDifficulty gameDifficulty = GameDifficulty.Easy;
    public List<Tuple<string, int>> petData = new();
    public int questProgress = 0;

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
        playerData.baseDamage = this.baseDemage;
        playerData.gameDifficulty = this.gameDifficulty;

        saveData.playerData = playerData;
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        this.Username = saveData.playerData.username;
        this.baseDemage = saveData.playerData.baseDamage;
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

    public void AddOrUpdatePet(string petName)
    {
        if (petData == null)
        {
            petData = new List<Tuple<string, int>>();
        }

        var pet = petData.FirstOrDefault(t => t.Item1 == petName);
        if (pet != null)
        {
            // Pet exists, increment the amount
            int index = petData.IndexOf(pet);
            petData[index] = new Tuple<string, int>(pet.Item1, pet.Item2 + 1);
        }
        else
        {
            // Pet does not exist
            petData.Add(new Tuple<string, int>(petName, 1));
        }
    }

    public void DecrementPet(string petName)
    {
        // If is null or empty
        if (petData == null || !petData.Any())
        {
            return;
        }

        var pet = petData.FirstOrDefault(t => t.Item1 == petName);
        if (pet != null)
        {
            int index = petData.IndexOf(pet);
            if (pet.Item2 - 1 > 0)
            {
                petData[index] = new Tuple<string, int>(pet.Item1, pet.Item2 - 1);
            }
            else
            {
                petData.RemoveAt(index);
            }
        }
    }

}
