using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float DistanceTraveled { get; private set; }
    public int MinutesPlayed { get; private set; } 

    private void OnEnable()
    {
        if (TimeManager.instance != null)
            TimeManager.instance.OnMinutePassed += UpdateMinutesPlayed;
    }

    private void UpdateMinutesPlayed(int minutes)
    {
        MinutesPlayed = minutes;
    }

    public void AddDistance(float distance)
    {
        DistanceTraveled += distance;
    }
}
