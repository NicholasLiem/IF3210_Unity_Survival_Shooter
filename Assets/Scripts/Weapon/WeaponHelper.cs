using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponHelper
{
    public static bool IsPartOfPlayer(Transform transform)
    {
        Transform currentParent = transform.parent;
        while (currentParent != null)
        {
            if (currentParent.CompareTag("Player"))
            {
                return true;
            }
            currentParent = currentParent.parent;
        }
        return false;
    }
}

