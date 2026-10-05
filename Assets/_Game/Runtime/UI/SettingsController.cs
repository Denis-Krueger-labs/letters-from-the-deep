using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public const string MasterVolumeKey = "MasterVolume";
    public const string MusicVolumeKey = "MusicVolume";
    public const string SfxVolumeKey = "SfxVolume";

    private const string FullscreenKey = "Fullscreen";
    private const string ResolutionWidthKey = "ResolutionWidth";
    private const string ResolutionHeightKey = "ResolutionHeight";

    [Header("Audio")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Display")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private readonly List<Resolution> availableResolutions = new();

    private void OnEnable()
    {
        InitialiseVolumeSliders();
        InitialiseFullscreenToggle();
        InitialiseResolutionDropdown();

        RegisterListeners();
    }

    private void OnDisable()
    {
        UnregisterListeners();
    }

    private void InitialiseVolumeSliders()
    {
        InitialiseSlider(
            masterVolumeSlider,
            PlayerPrefs.GetFloat(MasterVolumeKey, 1f)
        );

        InitialiseSlider(
            musicVolumeSlider,
            PlayerPrefs.GetFloat(MusicVolumeKey, 1f)
        );

        InitialiseSlider(
            sfxVolumeSlider,
            PlayerPrefs.GetFloat(SfxVolumeKey, 1f)
        );
    }

    private void InitialiseSlider(Slider slider, float value)
    {
        if (slider == null)
        {
            return;
        }

        slider.minValue = 0f;
        slider.maxValue = 1f;

        slider.SetValueWithoutNotify(value);
    }

    private void InitialiseFullscreenToggle()
    {
        if (fullscreenToggle == null)
        {
            return;
        }

        bool fullscreen =
            PlayerPrefs.GetInt(
                FullscreenKey,
                Screen.fullScreen ? 1 : 0
            ) == 1;

        fullscreenToggle.SetIsOnWithoutNotify(fullscreen);

        Screen.fullScreen = fullscreen;
    }

    private void InitialiseResolutionDropdown()
    {
        if (resolutionDropdown == null)
        {
            return;
        }

        availableResolutions.Clear();

        foreach (Resolution resolution in Screen.resolutions)
        {
            bool alreadyAdded = availableResolutions.Exists(
                existing =>
                    existing.width == resolution.width &&
                    existing.height == resolution.height
            );

            if (!alreadyAdded)
            {
                availableResolutions.Add(resolution);
            }
        }

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int savedWidth =
            PlayerPrefs.GetInt(
                ResolutionWidthKey,
                Screen.currentResolution.width
            );

        int savedHeight =
            PlayerPrefs.GetInt(
                ResolutionHeightKey,
                Screen.currentResolution.height
            );

        int selectedIndex = 0;

        for (int i = 0; i < availableResolutions.Count; i++)
        {
            Resolution resolution = availableResolutions[i];

            options.Add(
                $"{resolution.width} x {resolution.height}"
            );

            if (
                resolution.width == savedWidth &&
                resolution.height == savedHeight
            )
            {
                selectedIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.SetValueWithoutNotify(selectedIndex);
        resolutionDropdown.RefreshShownValue();
    }

    private void RegisterListeners()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.onValueChanged.AddListener(
                SetMasterVolume
            );
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(
                SetMusicVolume
            );
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.AddListener(
                SetSfxVolume
            );
        }

        if (fullscreenToggle != null)
        {
            fullscreenToggle.onValueChanged.AddListener(
                SetFullscreen
            );
        }

        if (resolutionDropdown != null)
        {
            resolutionDropdown.onValueChanged.AddListener(
                SetResolution
            );
        }
    }

    private void UnregisterListeners()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.onValueChanged.RemoveListener(
                SetMasterVolume
            );
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.RemoveListener(
                SetMusicVolume
            );
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.RemoveListener(
                SetSfxVolume
            );
        }

        if (fullscreenToggle != null)
        {
            fullscreenToggle.onValueChanged.RemoveListener(
                SetFullscreen
            );
        }

        if (resolutionDropdown != null)
        {
            resolutionDropdown.onValueChanged.RemoveListener(
                SetResolution
            );
        }
    }

    private void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, value);
        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
        PlayerPrefs.Save();
    }

    private void SetSfxVolume(float value)
    {
        PlayerPrefs.SetFloat(SfxVolumeKey, value);
        PlayerPrefs.Save();
    }

    private void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;

        PlayerPrefs.SetInt(
            FullscreenKey,
            fullscreen ? 1 : 0
        );

        PlayerPrefs.Save();
    }

    private void SetResolution(int resolutionIndex)
    {
        if (
            resolutionIndex < 0 ||
            resolutionIndex >= availableResolutions.Count
        )
        {
            return;
        }

        Resolution resolution =
            availableResolutions[resolutionIndex];

        Screen.SetResolution(
            resolution.width,
            resolution.height,
            Screen.fullScreen
        );

        PlayerPrefs.SetInt(
            ResolutionWidthKey,
            resolution.width
        );

        PlayerPrefs.SetInt(
            ResolutionHeightKey,
            resolution.height
        );

        PlayerPrefs.Save();
    }
}