using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private float timePlayed;
    private int secondsPassed = 0;
    private float secondCountdown = 1f;

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
        secondCountdown -= Time.deltaTime;

        if (secondCountdown <= 0)
        {
            secondCountdown += 1f;
            secondsPassed++;
            GameEventsManager.Instance.miscEvents.TriggerSecondPassed();

            if (secondsPassed >= 60)
            {
                secondsPassed = 0;
                GameEventsManager.Instance.miscEvents.TriggerMinutePassed();
            }
        }
    }

    public float GetTimePlayedMinutes()
    {
        return timePlayed / 60f;
    }

    public float GetTimePlayedSeconds()
    {
        return timePlayed;
    }
}
