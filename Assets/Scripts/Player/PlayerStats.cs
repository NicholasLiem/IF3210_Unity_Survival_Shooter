using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float ShotAccuracy {get; private set;}
    public float DistanceTraveled { get; private set; }
    public int MinutesPlayed { get; private set; } 
    public int EnemiesKilled {get; private set;}
    public int OrbsCollected {get; private set;}
    public int TotalGoldCollected {get; private set;}

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.OnMinutePassed += UpdateMinutesPlayed;
    }

    private void UpdateMinutesPlayed(int minutes)
    {
        Debug.Log("Minutes played: " + minutes);
        MinutesPlayed = minutes;
    }

    public void AddDistance(float distance)
    {
        DistanceTraveled += distance;
    }
}
