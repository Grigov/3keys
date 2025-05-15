using UnityEngine.UI;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject fElement;
    public GameObject enter0;
    public GameObject enter1;
    public GameObject InfoPanel;
    public GameObject endPanel;
    private bool isEndPanelNear = false;
    private bool isPlayerNear = false;
    private bool isShopUINear = false;
    private bool isEnterNear = false;
    private bool isEnter1Near = false;

    void Start()
    {
        InfoPanel.SetActive(true);
    }

    public void ExitInfoPanel()
    {
        InfoPanel.SetActive(false);
    }



    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && isShopUINear)
        {
            shopUI.SetActive(!shopUI.activeSelf);
            ShopUI.Instance.ShowSellMenu();
            ShopUI.Instance.ShowBuyMenu();
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && isEnterNear)
        {
            enter0.SetActive(!enter0.activeSelf);
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && isEnter1Near)
        {
            enter1.SetActive(!enter1.activeSelf);
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && isEndPanelNear)
        {
            if (DataPlayer.keys >= 3)
            {
                endPanel.SetActive(!endPanel.activeSelf);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shop") || other.CompareTag("Enter") || other.CompareTag("Enter1") || other.CompareTag("End"))
        {
            isPlayerNear = true;
            fElement.SetActive(true);
            if (other.CompareTag("Shop"))
            {
                isShopUINear = true;
            }
            else if (other.CompareTag("Enter"))
            {
                isEnterNear = true;
            }
            else if (other.CompareTag("Enter1"))
            {
                isEnter1Near = true;
            }
            else if (!other.CompareTag("End"))
            {
                isEndPanelNear = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shop") || other.CompareTag("Enter") || other.CompareTag("Enter1") || other.CompareTag("End"))
        {
            isPlayerNear = false;
            fElement.SetActive(false);
            if (other.CompareTag("Shop"))
            {
                shopUI.SetActive(false);
                isShopUINear = false;
            }
            else if (other.CompareTag("Enter"))
            {
                enter0.SetActive(false);
                isEnterNear = false;
            }
            else if (other.CompareTag("Enter1"))
            {
                enter1.SetActive(false);
                isEnter1Near = false;
            }
            else if (other.CompareTag("End"))
            {
                endPanel.SetActive(false);
                isEndPanelNear = false;
            }
        }
    }
}