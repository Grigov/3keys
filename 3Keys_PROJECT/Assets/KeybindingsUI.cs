using UnityEngine;
using UnityEngine.UI;

public class KeybindingsUI : MonoBehaviour
{
    public Text dashKeyText;
    public Text attackKeyText;
    public Text runKeyText;

    private string keyToRebind;

    void Start()
    {
        UpdateUI();
    }

    public void RebindKey(string action)
    {
        keyToRebind = action;
        dashKeyText.text = attackKeyText.text = runKeyText.text = "Press key...";
    }

    void OnGUI()
    {
        if (keyToRebind == null) return;

        Event e = Event.current;
        if (e.isKey)
        {
            Keybindings.SaveKey(keyToRebind, e.keyCode);
            keyToRebind = null;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        dashKeyText.text = Keybindings.keys["Dash"].ToString();
        attackKeyText.text = Keybindings.keys["Attack"].ToString();
        runKeyText.text = Keybindings.keys["Run"].ToString();
    }
}
