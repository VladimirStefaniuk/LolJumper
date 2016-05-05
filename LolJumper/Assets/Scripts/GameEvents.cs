using System;
using System.Collections;

public static class GameEvents { 
    /// <summary>
    /// Camera saw the new row
    /// </summary>
    public static Action OnCameraRowEnter;
    /// <summary>
    /// Row become outside of the screen
    /// </summary>
    public static Action OnCameraRowExit;
    /// <summary>
    /// Player fall
    /// </summary>
    public static Action OnPlayerFall;
}


public static class ActionExtention
{
    public static bool TryAction(this Action value)
    {
        if (value != null)
        {
            value();
            return true;
        }
        else
        {
            return false;
        }
    }
}
