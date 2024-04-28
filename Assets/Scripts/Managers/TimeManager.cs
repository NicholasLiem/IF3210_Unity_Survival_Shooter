using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public event System.Action<int> OnMinutePassed;

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
            OnMinutePassed?.Invoke((int)(timePlayed / 60f));
        }
    }

    public float GetTimePlayedMinutes()
    {
        return timePlayed / 60f;
    }
}
