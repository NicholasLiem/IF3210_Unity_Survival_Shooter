using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledEvents
{
    public event Action<string> OnEnemyKilled;
    public void TriggerEnemyKilled(string type)
    {
        OnEnemyKilled?.Invoke(type);
    }
}
