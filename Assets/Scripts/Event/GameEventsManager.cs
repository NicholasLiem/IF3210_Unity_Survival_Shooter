using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public delegate void OnReady();
    public static event OnReady Ready;
    public static GameEventsManager Instance;
    public MiscEvents miscEvents;
    public EnemyKilledEvents enemyKilledEvents;
    public PlayerActionEvents playerActionEvents;
    public QuestEvents questEvents;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        miscEvents = new MiscEvents();
        enemyKilledEvents = new EnemyKilledEvents();
        playerActionEvents = new PlayerActionEvents();
        questEvents = new QuestEvents();
        Ready?.Invoke();
        Debug.Log("GameEventsManager initialized: " + gameObject.name);
    }
}
