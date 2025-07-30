using System.Collections;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioConfig _config;

    private AudioMixerController _mixerController;
    private AudioSettingsManager _settingsManager;
    private SoundPlayer _soundPlayer;

    public bool Muted { get; set; } = false;

    private void Awake()
    {
        InitializeComponents();
        SetupAudioSources();
        StartCoroutine(DelayedInitialization());
    }

    private void InitializeComponents()
    {
        _mixerController = new AudioMixerController(_config.MasterMixer);
        _settingsManager = new AudioSettingsManager(_mixerController);
        _soundPlayer = new SoundPlayer(_config.Sounds, this);
    }

    private void SetupAudioSources()
    {
        foreach (Sound sound in _config.Sounds)
        {
            sound.Source.Clear();
            for (int i = 0; i < sound.NumberOfSource; i++)
            {
                AudioSource source = AudioSourceFactory.CreateAudioSource(gameObject, sound, _config.Mixers[sound.Group]);
                sound.Source.Add(source);
            }
        }
    }

    private IEnumerator DelayedInitialization()
    {
        yield return new WaitForSeconds(0.1f);
    }

    public void PlaySound(SoundType soundType) => _soundPlayer.Play(soundType);
    public void StopSound(SoundType soundType) => _soundPlayer.Stop(soundType);
    public bool IsSoundPlaying(SoundType soundType) => _soundPlayer.IsPlaying(soundType);

    public void SetVolume(SoundGroup group, float volume) => _settingsManager.SetVolume(group, volume);
    public float GetVolume(SoundGroup group) => _settingsManager.GetVolume(group);

    public void SetMasterVolume(float volume) => SetVolume(SoundGroup.None, volume);
    public void SetMusicVolume(float volume) => SetVolume(SoundGroup.Music, volume);
    public void SetSFXVolume(float volume) => SetVolume(SoundGroup.SFX, volume);
    public void SetUIVolume(float volume) => SetVolume(SoundGroup.UI, volume);

    public void MuteAll() => SetMasterVolume(-80f);
    public void UnmuteAll() => SetMasterVolume(_settingsManager.GetVolume(SoundGroup.None));
}
