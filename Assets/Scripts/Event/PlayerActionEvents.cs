using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionEvents
{
    public event Action OnShotFired;

    public event Action<bool> OnShotHit;

    public void TriggerShotFired()
    {
        OnShotFired?.Invoke();
    }

    public void TriggerShotHit(bool hit)
    {
        OnShotHit?.Invoke(hit);
    }
}
