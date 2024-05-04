using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event Action<int> OnMinutePassed;
    public event Action<string> OnOrbsCollected;
    public void TriggerMinutePassed(int minutes)
    {
        OnMinutePassed?.Invoke(minutes);
    }

    public void TriggerOrbsCollected(string orbsType){
        OnOrbsCollected?.Invoke(orbsType);
    }
}
