using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private float timePlayed;
    private float minuteCountdown = 60f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        timePlayed += Time.deltaTime;
        minuteCountdown -= Time.deltaTime;

        if (minuteCountdown <= 0)
        {
            minuteCountdown = 60f;
            if (GameEventsManager.Instance == null || GameEventsManager.Instance.miscEvents == null)
            {
                Debug.LogError("GameEventsManager or miscEvents is not initialized");
                return;
            }

            GameEventsManager.Instance.miscEvents.TriggerMinutePassed((int)(timePlayed / 60f));
        }
    }

    public float GetTimePlayedMinutes()
    {
        return timePlayed / 60f;
    }
}
