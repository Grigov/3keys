using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Stamina
{
    private static float _stamina = 40f;

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
}
