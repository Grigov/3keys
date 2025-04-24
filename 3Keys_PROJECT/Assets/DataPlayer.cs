using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPlayer
{
    private static float _stamina = 40f;
    private static int _health = 100;
    private static int _money = 0;

    public static float stamina
    {
        get
        {
            return _stamina;
        }

        set 
        {
            _stamina = value;
        }
    }
    public static int health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }
    public static int money
    {
        get
        {
            return _money;
        }

        set
        {
            _money = value;
        }
    }
}
