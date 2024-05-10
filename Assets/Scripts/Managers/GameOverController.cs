using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameOverController : MonoBehaviour
{
    private PlayerStats playerStats;
    // Upper
    public TMP_Text usernameText;

    // Left-Side
    public TMP_Text scoreText;
    public TMP_Text goldCollectedText;
    public TMP_Text timePlayedText;
    public TMP_Text enemiesKilledText;

    // Right-Side
    public TMP_Text distanceText;
    public TMP_Text shotAccuracyText;
    public TMP_Text totalShotsFiredText;
    public TMP_Text orbsCollectedText;

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

        BindComponentToVariables();
        UpdateUI();
    }

    void BindComponentToVariables()
    {
        usernameText = FindComponentInChildByName<TMPro.TextMeshProUGUI>("UsernameText");
        scoreText = FindComponentInChildByName<TMPro.TextMeshProUGUI>("StatisticsScoreText");
        goldCollectedText = FindComponentInChildByName<TMPro.TextMeshProUGUI>("StatisticsGoldText");
        timePlayedText = FindComponentInChildByName<TMPro.TextMeshProUGUI>("StatisticsTimePlayedText");
        enemiesKilledText = FindComponentInChildByName<TMPro.TextMeshProUGUI>("StatisticsEnemiesKilledText");
        distanceText = FindComponentInChildByName<TMPro.TextMeshProUGUI>("StatisticsDistanceText");
        shotAccuracyText = FindComponentInChildByName<TMPro.TextMeshProUGUI>("StatisticsShotAccuracyText");
        totalShotsFiredText = FindComponentInChildByName<TMPro.TextMeshProUGUI>("StatisticsTotalShotsFiredText");
        orbsCollectedText = FindComponentInChildByName<TMPro.TextMeshProUGUI>("StatisticsOrbsCollectedText");
    }

    void UpdateUI()
    {
        if (playerStats == null) return;

        if (usernameText != null)
            usernameText.text = $"Username: {GameManager.Instance.Username}";
        if (scoreText != null)
            scoreText.text = $"Local Score: {GameManager.Instance.localScore}";
        if (goldCollectedText != null)
            goldCollectedText.text = $"Local Gold Collected: {GameManager.Instance.localGoldCollected}";
        if (timePlayedText != null)
            timePlayedText.text = $"Time Played: {playerStats.GetFormattedTimePlayed()}";
        if (enemiesKilledText != null)
            enemiesKilledText.text = GenerateStatsText(playerStats.EnemyKillCount, "Enemies Killed");
        if (distanceText != null)
            distanceText.text = $"Distance: {playerStats.DistanceTraveled} km";
        if (shotAccuracyText != null)
            shotAccuracyText.text = $"Shot Accuracy: {playerStats.ShotAccuracy * 100:F2}%";
        if (totalShotsFiredText != null)
            totalShotsFiredText.text = $"Total Shots Fired: {playerStats.TotalShotsFired}";
        if (orbsCollectedText != null)
            orbsCollectedText.text = GenerateStatsText(playerStats.OrbsCollected, "Orbs Collected");
    }

    T FindComponentInChildByName<T>(string name) where T : Component
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.name == name)
            {
                T component = child.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }
        }
        return null;
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
