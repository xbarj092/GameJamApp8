using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioMixerController
{
    private readonly AudioMixerGroup _masterMixer;
    private readonly Dictionary<SoundGroup, string> _mixerNames;

    public AudioMixerController(AudioMixerGroup masterMixer)
    {
        _masterMixer = masterMixer;
        _mixerNames = new Dictionary<SoundGroup, string>
        {
            { SoundGroup.None, "MasterVolume" },
            { SoundGroup.SFX, "SFXVolume" },
            { SoundGroup.Music, "MusicVolume" },
            { SoundGroup.UI, "UIVolume" }
        };
    }

    public void SetVolume(SoundGroup group, float volume)
    {
        if (_mixerNames.TryGetValue(group, out string parameterName))
        {
            _masterMixer.audioMixer.SetFloat(parameterName, volume);
        }
    }

    public float GetVolume(SoundGroup group)
    {
        if (_mixerNames.TryGetValue(group, out string parameterName))
        {
            _masterMixer.audioMixer.GetFloat(parameterName, out float volume);
            return volume;
        }

        return 0f;
    }

    public void ApplySettings(Dictionary<SoundGroup, float> settings)
    {
        foreach (KeyValuePair<SoundGroup, float> setting in settings)
        {
            SetVolume(setting.Key, setting.Value);
        }
    }
}
