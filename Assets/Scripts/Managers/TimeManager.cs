using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private float secondCounter = 1.0f;
    private int totalSeconds = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        secondCounter -= Time.deltaTime;
        if (secondCounter <= 0)
        {
            secondCounter += 1.0f;
            totalSeconds++;
            GameManager.Instance.PlayerStats.AddSecondsPassed();

            if (totalSeconds % 60 == 0)
            {
                GameEventsManager.Instance.miscEvents.TriggerMinutePassed();
            }
        }
    }
}
