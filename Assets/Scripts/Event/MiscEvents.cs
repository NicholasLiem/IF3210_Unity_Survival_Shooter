using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event Action<int> OnMinutePassed;
    public void TriggerMinutePassed(int minutes)
    {
        OnMinutePassed?.Invoke(minutes);
    }
}
