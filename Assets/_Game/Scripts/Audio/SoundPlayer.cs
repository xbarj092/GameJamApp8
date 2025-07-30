using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundPlayer
{
    private readonly List<Sound> _sounds;
    private readonly MonoBehaviour _coroutineRunner;

    public SoundPlayer(List<Sound> sounds, MonoBehaviour coroutineRunner)
    {
        _sounds = sounds;
        _coroutineRunner = coroutineRunner;
    }

    public void Play(SoundType soundType)
    {
        Sound sound = FindSound(soundType);
        if (sound == null)
        {
            return;
        }

        AudioSource availableSource = GetAvailableAudioSource(sound);
        if (availableSource != null)
        {
            _coroutineRunner.StartCoroutine(PlayWithVariation(availableSource, sound.VariablePitch));
        }
    }

    public void Stop(SoundType soundType)
    {
        Sound sound = FindSound(soundType);
        sound?.Source.ForEach(source => source.Stop());
    }

    public bool IsPlaying(SoundType soundType)
    {
        Sound sound = FindSound(soundType);
        return sound?.Source.Exists(source => source.isPlaying) ?? false;
    }

    private Sound FindSound(SoundType soundType)
    {
        return _sounds.FirstOrDefault(sound => sound.Name == soundType);
    }

    private AudioSource GetAvailableAudioSource(Sound sound)
    {
        return sound.Source.Find(source => !source.isPlaying) ?? sound.Source.FirstOrDefault();
    }

    private IEnumerator PlayWithVariation(AudioSource source, bool useVariablePitch)
    {
        float originalPitch = source.pitch;

        if (useVariablePitch)
        {
            source.pitch += Random.Range(-0.08f, 0.09f);
        }

        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        source.pitch = originalPitch;
    }
}
