using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    public Slider volumeSlider;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public GameObject settingsPanel;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
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
}
