using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;

[Serializable]
public class AudioConfig
{
    public bool SpatialBlend;
    public SerializedDictionary<SoundGroup, AudioMixerGroup> Mixers = new();
    public AudioMixerGroup MasterMixer;
    public List<Sound> Sounds;
}
