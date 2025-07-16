using Godot;
using System;

public static class Utils
{
    public static string GetKeyName(string actionName)
    {
        var events = InputMap.ActionGetEvents(actionName);
        foreach (var inputEvent in events)
        {
            if (inputEvent is InputEventKey keyEvent)
            {
                var keyEnum = keyEvent.PhysicalKeycode;
                return keyEnum.ToString().Replace("Key.", "");
            }
        }
        return "None";
    }
}
