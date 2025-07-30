using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceFactory
{
    public static AudioSource CreateAudioSource(GameObject parent, Sound sound, AudioMixerGroup mixerGroup)
    {
        AudioSource source = parent.AddComponent<AudioSource>();
        ConfigureAudioSource(source, sound, mixerGroup);
        return source;
    }

    private static void ConfigureAudioSource(AudioSource source, Sound sound, AudioMixerGroup mixerGroup)
    {
        source.playOnAwake = false;
        source.loop = sound.Loop;
        source.clip = sound.Clip;
        source.volume = sound.Volume;
        source.pitch = sound.Pitch;
        source.spatialBlend = sound.SpatialBlend;
        source.outputAudioMixerGroup = mixerGroup;
    }
}
