using System.Diagnostics;
using UnityEngine;

public static class DataPlayer
{
    private static float _stamina = 40f;
    private static int _health = 100;
    private static float _money = 0;
    private static int _baseS = 40;
    private static int _keys;
    private static float _priceKey = 50f;
    private static int _priceStSk = 10;
    private static int _counter = 3;

    public static void ResetData()
    {
        _stamina = 40f;
        _health = 100;
        _money = 0;
        _baseS = 40;
        _keys = 0;

        _priceKey = 50f;
        _priceStSk = 10;
        _counter = 3;

        PlayerHealth.Instance.maxHealth = 100f;
    }

    public static float stamina
    {
        get => _stamina;
        set => _stamina = Mathf.Clamp(value, 0, _baseS);
    }

    public static int health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, (int)PlayerHealth.Instance.maxHealth);
    }

    public static float money
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


    public static float priceKey
    {
        get => _priceKey;
        set => _priceKey = Mathf.Max(0, value);
    }
    public static int priceStSk
    {
        get => _priceStSk;
        set => _priceStSk = Mathf.Max(0, value);
    }
    public static int counter
    {
        get => _counter;
        set => _counter = Mathf.Max(0, value);
    }
}