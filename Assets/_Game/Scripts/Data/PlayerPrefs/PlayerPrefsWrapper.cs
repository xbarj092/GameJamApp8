using System;
using System.Collections.Generic;

[Serializable]
public class PlayerPrefsWrapper
{
    private PlayerPrefsHandler _playerPrefsHandler = new();

    private const string KEY_AUDIO_SETTINGS = "AudioSettings";

    public void SaveAudioSettings(Dictionary<SoundGroup, float> audioSettings)
    {
        _playerPrefsHandler.SaveData(KEY_AUDIO_SETTINGS, audioSettings);
    }

    public Dictionary<SoundGroup, float> LoadAudioSettings()
    {
        return _playerPrefsHandler.LoadData<Dictionary<SoundGroup, float>>(KEY_AUDIO_SETTINGS);
    }

    public void DeleteAudioSettings()
    {
        _playerPrefsHandler.DeleteData(KEY_AUDIO_SETTINGS);
    }
}
