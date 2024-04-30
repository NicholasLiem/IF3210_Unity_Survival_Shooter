using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameDifficulty : ushort
{
    Easy = 0,
    Medium = 1,
    Hard = 2
}

public class GameManagement : MonoBehaviour
{

    public static GameManagement Instance;

    public string Username { get; set; }
    public GameDifficulty gameDifficulty;

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

    void Start()
    {
        // Default value
        Username = "";
        gameDifficulty = GameDifficulty.Easy;
    }
}
