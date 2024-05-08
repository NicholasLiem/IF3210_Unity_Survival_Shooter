using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int score = 0;
    public int gold = 0;

    public int currentLevel = 1;
    public int shopSceneIndex = 8;
    public int MAX_PLAYABLE_SCENE = 7;

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

        SaveData.QuestData questData = new();

        questData.progress = this.questProgress;

        saveData.questData = questData;
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        this.Username = saveData.playerData.username;
        this.baseDemage = saveData.playerData.baseDamage;
        this.gameDifficulty = saveData.playerData.gameDifficulty;

        this.questProgress = saveData.questData.progress;
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
       
    public void AdvanceLevel(bool toShop = false)
    {
        if (toShop)
        {
            SceneManager.LoadScene(shopSceneIndex);
        }
        else
        {
            LoadLevel(currentLevel + 1);
        }
    }

    private void LoadLevel(int level)
    {
        currentLevel = level;
        if (currentLevel > MAX_PLAYABLE_SCENE)
        {
            // Reset scene level, score, gold and back to main menu
            currentLevel = 1;
            score = 0;
            gold = 0;
            SceneManager.LoadScene(0);
        } else
        {
            // Load next level in 
            SceneManager.LoadScene(currentLevel);
        }
    }
}
