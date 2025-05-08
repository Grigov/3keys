using UnityEngine;

public static class DataPlayer
{
    private static float _stamina = 40f;
    private static int _health = 100;
    private static int _money = 0;
    private static int _baseS = 40;
    private static int _keys;

    public static void ResetData()
    {
        _stamina = 40f;
        _health = 100;
        _money = 0;
        _baseS = 40;
        _keys = 0;
    }

    public static float stamina
    {
        get => _stamina;
        set => _stamina = Mathf.Clamp(value, 0, _baseS);
    }

    public static int health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, 100);
    }

    public static int money
    {
        get => _money;
        set => _money = Mathf.Max(0, value);
    }

    public static int baseS
    {
        get => _baseS;
        set => _baseS = Mathf.Max(10, value);
    }
    public static int keys
    {
        get => _keys;
        set => _keys = Mathf.Max(0, value);
    }
}