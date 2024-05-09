using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatsDisplay : MonoBehaviour
{
    private PlayerStats playerStats;
    public Text goldCollectedText;
    public Text minutesPlayedText;
    public Text shotAccuracyText;
    // public Text distanceText;
    // public Text orbsCollectedText;
    // public Text enemiesKilledText;

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

        minutesPlayedText = FindComponentInChildByName<Text>("MinutesPlayedText");
        shotAccuracyText = FindComponentInChildByName<Text>("ShotAccuracyText");
        goldCollectedText = FindComponentInChildByName<Text>("GoldCollectedText");
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
        if (minutesPlayedText != null)
            minutesPlayedText.text = $"Minutes Played: {playerStats.MinutesPlayed}";
        if (goldCollectedText != null)
            goldCollectedText.text = $"Gold Collected: {playerStats.GoldCollected}";
        if (shotAccuracyText != null)
            shotAccuracyText.text = $"Shot Accuracy: {playerStats.ShotAccuracy * 100:F2}%";
        // if (orbsCollectedText != null)
        //     orbsCollectedText.text = GenerateStatsText(playerStats.OrbsCollected, "Orbs Collected");
        // if (enemiesKilledText != null)
        //     enemiesKilledText.text = GenerateStatsText(playerStats.EnemyKillCount, "Enemies Killed");
        // if (distanceText != null)
        //     distanceText.text = $"Distance Traveled: {playerStats.DistanceTraveled:F2}m";
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
