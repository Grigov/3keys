using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Potion : MonoBehaviour
{
    public string potionName;
    public float healingCount;

    public virtual void Healing(Health health)
    {
        if (DataPlayer.health < health.maxHealth)
        {

        }
    }
}