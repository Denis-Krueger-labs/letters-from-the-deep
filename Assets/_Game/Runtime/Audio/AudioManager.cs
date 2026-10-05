using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        LoadVolumeSettings();
        ApplyVolumes();
    }

    private void LoadVolumeSettings()
    {
        masterVolume = PlayerPrefs.GetFloat(
            SettingsController.MasterVolumeKey,
            1f
        );

        musicVolume = PlayerPrefs.GetFloat(
            SettingsController.MusicVolumeKey,
            1f
        );

        sfxVolume = PlayerPrefs.GetFloat(
            SettingsController.SfxVolumeKey,
            1f
        );
    }

    private void ApplyVolumes()
    {
        if (musicSource != null)
        {
            musicSource.volume =
                masterVolume * musicVolume;
        }

        if (sfxSource != null)
        {
            sfxSource.volume =
                masterVolume * sfxVolume;
        }
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat(
            SettingsController.MasterVolumeKey,
            masterVolume
        );

        PlayerPrefs.Save();

        ApplyVolumes();
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat(
            SettingsController.MusicVolumeKey,
            musicVolume
        );

        PlayerPrefs.Save();

        ApplyVolumes();
    }

    public void SetSfxVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);

        PlayerPrefs.SetFloat(
            SettingsController.SfxVolumeKey,
            sfxVolume
        );

        PlayerPrefs.Save();

        ApplyVolumes();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null)
        {
            return;
        }

        if (musicSource.clip == clip &&
            musicSource.isPlaying)
        {
            return;
        }

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PlaySfx(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip);
    }
}