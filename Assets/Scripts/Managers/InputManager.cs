using UnityEngine;

public static class InputManager
{
    public static bool CanProcessInput = true;

    public static void EnableInput()
    {
        CanProcessInput = true;
    }

    public static void DisableInput()
    {
        CanProcessInput = false;
    }
}
