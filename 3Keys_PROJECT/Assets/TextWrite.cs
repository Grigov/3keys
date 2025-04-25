using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextWrite : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text staminaText;
    [SerializeField] private Text moneyText;
    void Start()
    {

    }
    void Update()
    {
        healthText.text = $"Health: {DataPlayer.health} / 100";
        staminaText.text = $"Stamina: {Mathf.Round(DataPlayer.stamina)} / {DataPlayer.baseS}";
        moneyText.text = $"Money: {DataPlayer.money}";
    }
}
