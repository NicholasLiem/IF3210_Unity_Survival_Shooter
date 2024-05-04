using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event Action<int> OnMinutePassed;
    public event Action<string> OnOrbsCollected;
    public event Action<int> OnGoldCollected;
    public void TriggerMinutePassed(int minutes)
    {
        OnMinutePassed?.Invoke(minutes);
    }

    public void TriggerOrbsCollected(string orbsType){
        OnOrbsCollected?.Invoke(orbsType);
    }

    public void TriggerGoldCollected(int amount)
    {
        OnGoldCollected?.Invoke(amount);
    }
}
