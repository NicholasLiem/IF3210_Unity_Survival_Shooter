using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatsDisplay : MonoBehaviour
{
    public PlayerStats playerStats;
    public Text goldCollectedText;
    public Text minutesPlayedText;
    public Text shotAccuracyText;
    // public Text distanceText;
    // public Text orbsCollectedText;
    // public Text enemiesKilledText;

    void Awake()
    {
        distanceText = FindComponentInChildByName<Text>("DistanceTraveledText");
        minutesPlayedText = FindComponentInChildByName<Text>("MinutesPlayedText");
        shotAccuracyText = FindComponentInChildByName<Text>("ShotAccuracyText");
        // goldCollectedText = FindComponentInChildByName<Text>("GoldCollectedText");
        // orbsCollectedText = FindComponentInChildByName<Text>("OrbsCollectedText");
        // enemiesKilledText = FindComponentInChildByName<Text>("EnemiesKilledText");
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
        if (minutesPlayedText != null)
            minutesPlayedText.text = $"Minutes Played: {playerStats.MinutesPlayed}";
        if (goldCollectedText != null)
            goldCollectedText.text = $"Gold Collected: {playerStats.GoldCollected}";
        // if (orbsCollectedText != null)
        //     orbsCollectedText.text = GenerateStatsText(playerStats.OrbsCollected, "Orbs Collected");
        // if (enemiesKilledText != null)
        //     enemiesKilledText.text = GenerateStatsText(playerStats.EnemyKillCount, "Enemies Killed");
        // if (distanceText != null)
        //     distanceText.text = $"Distance Traveled: {playerStats.DistanceTraveled:F2}m";
        if (shotAccuracyText != null)
            shotAccuracyText.text = $"Shot Accuracy: {playerStats.ShotAccuracy * 100:F2}%";
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
