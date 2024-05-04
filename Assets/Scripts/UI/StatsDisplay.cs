using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    public PlayerStats playerStats;
    public Text goldCollectedText;

    void Awake()
    {
        // distanceText = GetComponentInChildren<TMP_Text>(true); // Set 'true' if you expect them to be inactive at startup
        // minutesPlayedText = GetComponentInChildren<TMP_Text>(true);
        goldCollectedText = GetComponentInChildren<Text>();
        // shotAccuracyText = GetComponentInChildren<TMP_Text>(true);

        CheckComponents();
    }

    void Update()
    {
        UpdateUI();
    }

    void CheckComponents()
    {
        // if (distanceText == null)
        //     Debug.LogError("Distance Text is not assigned!");
        // if (minutesPlayedText == null)
        //     Debug.LogError("Minutes Played Text is not assigned!");
        if (goldCollectedText == null)
            Debug.LogError("Gold Collected Text is not assigned!");
        // if (shotAccuracyText == null)
        //     Debug.LogError("Shot Accuracy Text is not assigned!");
    }

    void UpdateUI()
    {
        // if (distanceText != null)
        //     distanceText.text = $"Distance Traveled: {playerStats.DistanceTraveled} meters";
        // if (minutesPlayedText != null)
        //     minutesPlayedText.text = $"Minutes Played: {playerStats.MinutesPlayed}";
        if (goldCollectedText != null)
            goldCollectedText.text = $"Gold Collected: {playerStats.GoldCollected}";
        // if (shotAccuracyText != null)
        //     shotAccuracyText.text = $"Shot Accuracy: {playerStats.ShotAccuracy * 100}%";
    }
}
