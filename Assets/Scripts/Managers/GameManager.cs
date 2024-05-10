using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
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

    public GameEventsManager GameEventsManager;

    public PlayerStats PlayerStats;
    public QuestManager QuestManager;

    public int currentLevel = 1;
    public int shopSceneIndex = 8;
    public int MAX_PLAYABLE_SCENE = 7;

    public bool HavePlayed;

    public AudioMixer masterMixer;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        CheckForLevelInitialization();
    }

    private void CheckForLevelInitialization()
    {
        if (currentLevel % 2 == 0)
        {
            GameEventsManager.Instance.miscEvents.TriggerLevelAdvance(currentLevel);
            Debug.Log("Triggered Level Advance for level: " + currentLevel);
        }
    }

    private void Awake()
    {
        Debug.Log("GameManager Awake started");
        if (Instance == null)
        {
            Instance = this;
            PlayerStats = FindObjectOfType<PlayerStats>();
            QuestManager = FindObjectOfType<QuestManager>();
            GameEventsManager = FindObjectOfType<GameEventsManager>();
            if (PlayerStats == null && QuestManager == null && GameEventsManager == null)
            {
                Debug.Log("PlayerStats and QuestManager not found, creating new one.");
                GameObject stats = new GameObject("PlayerStats");
                PlayerStats = stats.AddComponent<PlayerStats>();
                GameObject gem = new GameObject("GameEventsManager");
                GameEventsManager = gem.AddComponent<GameEventsManager>();
                GameObject qm = new GameObject("QuestManager");
                QuestManager = qm.AddComponent<QuestManager>();
            }
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("GameManager instance already exists, destroying duplicate.");
            Destroy(gameObject);
        }
        Debug.Log("GameManager Awake completed with PlayerStats: " + PlayerStats);
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
        SaveData.PetData[] peDataArray = new SaveData.PetData[petData.Count];
        for (int i = 0; i < petData.Count; i++)
        {
            peDataArray[i] = new SaveData.PetData { name = petData[i].Item1, count = petData[i].Item2 };
        }
        playerData.petData = peDataArray;
        saveData.playerData = playerData;

        this.QuestManager.PopulateSaveData(saveData);
        saveData.questData.progress = this.questProgress;
        saveData.questData.currentLevel = this.currentLevel;
        Debug.Log("ULELELEL" + saveData.questData.questMap.Length);

        PlayerStats.PopulateSaveData(saveData);
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        this.Username = saveData.playerData.username;
        this.baseDemage = saveData.playerData.baseDamage;
        this.gameDifficulty = saveData.playerData.gameDifficulty;
        this.petData.Clear();
        foreach (SaveData.PetData item in saveData.playerData.petData)
        {
            this.petData.Add(new Tuple<string, int>(item.name, item.count));
        }

        this.QuestManager.LoadFromSaveData(saveData);
        this.questProgress = saveData.questData.progress;
        this.currentLevel = saveData.questData.currentLevel;

        PlayerStats.LoadFromSaveData(saveData);
    }

    public void SaveGame(int num)
    {
        SaveData sd = new();
        PopulateSaveData(sd);

        if (FileManager.WriteToFile(num + ".dat", sd.ToJson()))
        {
            Debug.Log("Save successful " + num);
        }
    }

    public void LoadGame(int num)
    {
        Debug.Log("LOADING");
        if (FileManager.LoadFromFile(num + ".dat", out var json))
        {
            SaveData sd = new();
            sd.LoadFromJson(json);

            LoadFromSaveData(sd);
            Debug.Log("Load complete");
            this.HavePlayed = true;

            AdvanceLevel(true);
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
        int nextLevel = currentLevel + 1;
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
            // PlayerStats.ResetStats();
            SceneManager.LoadScene(0);
        } else
        {
            // Load next level in 
            SceneManager.LoadScene(currentLevel);
        }
    }

    public void RestartGame()
    {
        currentLevel = 1;
        QuestManager.RestartQuest(currentLevel);
    }
}
