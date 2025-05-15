using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextWrite : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text staminaText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text keysText;
    void Start()
    {

    }
    void Update()
    {
        healthText.text = $"Здоровье: {DataPlayer.health} / {PlayerHealth.Instance.maxHealth}";
        staminaText.text = $"Выносливость: {Mathf.Round(DataPlayer.stamina)} / {DataPlayer.baseS}";
        moneyText.text = $"Златницы: {DataPlayer.money}ʓ";
        keysText.text = $"Ключи: {DataPlayer.keys} / 3";
    }
}
