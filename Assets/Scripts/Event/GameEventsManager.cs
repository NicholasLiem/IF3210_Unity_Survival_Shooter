using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance;
    public MiscEvents miscEvents;
    public EnemyKilledEvents enemyKilledEvents;
    public PlayerActionEvents playerActionEvents;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Another instance of GameEventsManager exists: " + gameObject.name + " destroying!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        miscEvents = new MiscEvents();
        enemyKilledEvents = new EnemyKilledEvents();
        playerActionEvents = new PlayerActionEvents();
        Debug.Log("GameEventsManager initialized: " + gameObject.name);
    }
}
