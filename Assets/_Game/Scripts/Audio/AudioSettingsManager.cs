using System.Collections.Generic;

public class AudioSettingsManager
{
    private readonly AudioMixerController _mixerController;
    private Dictionary<SoundGroup, float> _currentSettings;

    public AudioSettingsManager(AudioMixerController mixerController)
    {
        _mixerController = mixerController;
        LoadSettings();
    }

    public void SetVolume(SoundGroup group, float volume)
    {
        _currentSettings[group] = volume;
        _mixerController.SetVolume(group, volume);
        SaveSettings();
    }

    public float GetVolume(SoundGroup group)
    {
        return _currentSettings.TryGetValue(group, out float volume) ? volume : 0f;
    }

    private void LoadSettings()
    {
        _currentSettings = LocalDataStorage.Instance.PlayerPrefs.LoadAudioSettings();

        if (_currentSettings == null)
        {
            _currentSettings = GetDefaultSettings();
            SaveSettings();
        }

        _mixerController.ApplySettings(_currentSettings);
    }

    private void SaveSettings()
    {
        LocalDataStorage.Instance.PlayerPrefs.SaveAudioSettings(_currentSettings);
    }

    private Dictionary<SoundGroup, float> GetDefaultSettings()
    {
        return new Dictionary<SoundGroup, float>
        {
            { SoundGroup.None, 0f },
            { SoundGroup.Music, 0f },
            { SoundGroup.SFX, 0f },
            { SoundGroup.UI, 0f }
        };
    }
}
