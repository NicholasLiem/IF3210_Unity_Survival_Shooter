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
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.name == name)
            {
                return child.GetComponent<T>();
            }
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

    string GenerateStatsText(Dictionary<string, int> statsDictionary, string title)
    {
        string text = $"{title}:\n";
        foreach (KeyValuePair<string, int> entry in statsDictionary)
        {
            text += $"{entry.Key}: {entry.Value}\n";
        }
        return text;
    }
}
