using System;
using System.Collections.Generic;

[Serializable]
public class PlayerPrefsWrapper
{
    private PlayerPrefsHandler _playerPrefsHandler = new();

    private const string KEY_AUDIO_SETTINGS = "AudioSettings";
    private const string KEY_TUTORIAL = "Tutorial";

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

    public void SaveTutorial(bool tutorialCompleted)
    {
        _playerPrefsHandler.SaveData(KEY_TUTORIAL, tutorialCompleted);
    }

    public bool LoadTutorial()
    {
        return _playerPrefsHandler.LoadData<bool>(KEY_TUTORIAL);
    }

    public void DeleteTutorial()
    {
        _playerPrefsHandler.DeleteData(KEY_TUTORIAL);
    }
}
