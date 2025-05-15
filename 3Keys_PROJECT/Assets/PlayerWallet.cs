using System.Diagnostics;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddGold(int amount)
    {
        DataPlayer.money += amount;
    }
}
