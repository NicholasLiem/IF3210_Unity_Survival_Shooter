using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    public PlayerStats playerStats;
    public Text goldCollectedText;
    public Text minutesPlayedText;
    public Text distanceText;

    void Awake()
    {
        distanceText = FindComponentInChildByName<Text>("DistanceTraveledText");
        minutesPlayedText = FindComponentInChildByName<Text>("MinutesPlayedText");
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
        if (distanceText != null)
            distanceText.text = $"Distance Traveled: {playerStats.DistanceTraveled:F2}m";
        if (minutesPlayedText != null)
            minutesPlayedText.text = $"Minutes Played: {playerStats.MinutesPlayed}";
        if (goldCollectedText != null)
            goldCollectedText.text = $"Gold Collected: {playerStats.GoldCollected}";
    }
}
