using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event Action OnSecondPassed;
    public event Action OnMinutePassed;
    public event Action<string> OnOrbsCollected;
    public event Action<int> OnGoldCollected;

    public event Action<int> OnLevelAdvance;

    public void TriggerMinutePassed()
    {
        OnMinutePassed?.Invoke();
    }
    public void TriggerSecondPassed()
    {
        OnSecondPassed?.Invoke();
    }

    public void TriggerOrbsCollected(string orbsType){
        OnOrbsCollected?.Invoke(orbsType);
    }

    public void TriggerGoldCollected(int amount)
    {
        OnGoldCollected?.Invoke(amount);
    }

    public void TriggerLevelAdvance(int level)
    {
        Debug.Log("TriggerLevelAdvance called with level: " + level);
        if (OnLevelAdvance != null)
        {
            OnLevelAdvance(level);
        }
    }
}
