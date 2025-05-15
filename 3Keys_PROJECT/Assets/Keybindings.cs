using System.Collections.Generic;
using UnityEngine;

public static class Keybindings
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    static Keybindings()
    {
        LoadKeys();
    }

    public static void LoadKeys()
    {
        keys["Dash"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Dash", "Space"));
        keys["Attack"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Attack", "Mouse0"));
        keys["Run"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Key_Run", "LeftShift"));
        // можно добавить другие
    }

    public static void SaveKey(string action, KeyCode key)
    {
        keys[action] = key;
        PlayerPrefs.SetString("Key_" + action, key.ToString());
    }
}
