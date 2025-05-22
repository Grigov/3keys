using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    public Slider volumeSlider;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public GameObject settingsPanel;
    public InputField dashKeyInput;
    public Button resetDefaultsButton;

    void Start()
    {
        // Загрузить сохранённые настройки
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        qualityDropdown.value = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        ApplySettings();

        // Добавить обработчики
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);

        // Загрузка новых настроек
        dashKeyInput.text = Keybindings.keys["Dash"].ToString();

        // Обработчики
        dashKeyInput.onEndEdit.AddListener(OnDashKeyChanged);
        resetDefaultsButton.onClick.AddListener(ResetToDefaults);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        bool isPausing = !settingsPanel.activeSelf;

        Time.timeScale = isPausing ? 0 : 1;

        settingsPanel.SetActive(isPausing);
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void OnQualityChanged(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("Quality", index);
    }

    public void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void ApplySettings()
    {
        AudioListener.volume = volumeSlider.value;
        QualitySettings.SetQualityLevel(qualityDropdown.value);
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnDashKeyChanged(string key)
    {
        if (string.IsNullOrEmpty(key)) return;

        // Преобразуем строку в KeyCode
        if (System.Enum.TryParse(key, out KeyCode keyCode))
        {
            Keybindings.SaveKey("Dash", keyCode); // Используем SaveKey вместо UpdateKey
            PlayerPrefs.SetString("DashKey", key);
        }
        else
        {
            UnityEngine.Debug.LogError("Некорректная клавиша: " + key);
        }
    }

    public void ResetToDefaults()
    {
        // Сброс всех настроек
        volumeSlider.value = 1f;
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        fullscreenToggle.isOn = true;
        dashKeyInput.text = "Space";
        ApplySettings();
    }
}
