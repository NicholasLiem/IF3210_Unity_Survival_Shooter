using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatsDisplay : MonoBehaviour
{
    private PlayerStats playerStats;
    public Text goldCollectedText;
    public Text timePlayedText;
    public Text shotAccuracyText;
    public Text scoreText;

    void Awake()
    {
        if (GameManager.Instance != null && GameManager.Instance.PlayerStats != null)
        {
            playerStats = GameManager.Instance.PlayerStats;
        }
        else
        {
            Debug.LogError("GameManager or PlayerStats is not initialized.");
        }

        timePlayedText = FindComponentInChildByName<Text>("TimePlayedText");
        shotAccuracyText = FindComponentInChildByName<Text>("ShotAccuracyText");
        goldCollectedText = FindComponentInChildByName<Text>("GoldCollectedText");
        scoreText = FindComponentInChildByName<Text>("ScoreText");
    }

    void Update()
    {
        UpdateUI();
    }

    T FindComponentInChildByName<T>(string name) where T : Component
    {
        return FindComponentInChildByName<T>(transform, name);
    }

    T FindComponentInChildByName<T>(Transform parent, string name) where T : Component
    {
        if (parent.name == name)
        {
            T component = parent.GetComponent<T>();
            if (component != null)
                return component;
        }
        foreach (Transform child in parent)
        {
            T found = FindComponentInChildByName<T>(child, name);
            if (found != null)
                return found;
        }
        return null;
    }

    void UpdateUI()
    {
        if (playerStats == null) return;
        if (timePlayedText != null)
            timePlayedText.text = $"Time Played: {playerStats.GetFormattedTimePlayed()}";
        if (goldCollectedText != null)
            goldCollectedText.text = $"Gold Collected: {playerStats.GoldCollected}";
        if (shotAccuracyText != null)
            shotAccuracyText.text = $"Shot Accuracy: {playerStats.ShotAccuracy * 100:F2}%";
        if (scoreText != null)
            scoreText.text = $"Score: {playerStats.Score}";
    }
}
