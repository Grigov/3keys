using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemsStat : MonoBehaviour
{
    public static BuyItemsStat Ins;
    [SerializeField] private Text priceKeyText;
    [SerializeField] private Text priceStSkText;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RefreshPrises()
    {
        priceKeyText.text = $"Цена: {DataPlayer.priceKey}ʓ";
        priceStSkText.text = $"Цена: {DataPlayer.priceStSk}ʓ";
    }

    public void BuyKey()
    {
        if (DataPlayer.money >= DataPlayer.priceKey && DataPlayer.counter > 0)
        {
            DataPlayer.money -= DataPlayer.priceKey;
            DataPlayer.priceKey *= 1.4f;
            DataPlayer.priceKey = Mathf.Round(DataPlayer.priceKey);
            DataPlayer.keys += 1;
            DataPlayer.counter--;
        }
        RefreshPrises();
    }
    public void BuyStoneSkin()
    {
        if (DataPlayer.money >= DataPlayer.priceStSk && PlayerHealth.Instance != null)
        {
            DataPlayer.money -= DataPlayer.priceStSk;
            DataPlayer.priceStSk *= 2;
            PlayerHealth.Instance.maxHealth += 20f;
        }
        RefreshPrises();
    }
}
