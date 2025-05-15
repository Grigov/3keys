using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemsStat : MonoBehaviour
{
    [SerializeField] private int priceKey;
    [SerializeField] private int priceStSk;
    [SerializeField] private Text priceKeyText;
    [SerializeField] private Text priceStSkText;

    void Start()
    {
        priceKey = 50;
        priceStSk = 10;
    }

    public void RefreshPrises()
    {
        priceKeyText.text = $"Цена: {priceKey}ʓ";
        priceStSkText.text = $"Цена: {priceStSk}ʓ";
    }

    public void BuyKey()
    {
        if (DataPlayer.money >= priceKey)
        {
            DataPlayer.money -= priceKey;
            priceKey *= 2;
            DataPlayer.keys += 1;
        }
        RefreshPrises();
    }
    public void BuyStoneSkin()
    {
        if (DataPlayer.money >= priceStSk && PlayerHealth.Instance != null)
        {
            DataPlayer.money -= priceStSk;
            priceStSk *= 2;
            PlayerHealth.Instance.maxHealth += 20f;
        }
        RefreshPrises();
    }
}
