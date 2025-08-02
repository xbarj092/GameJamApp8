using UnityEngine;
using UnityEngine.UI;

public class OptionsPopup : MonoBehaviour
{
    [SerializeField] private Toggle _muteButton;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _uiSlider;

    private void Start()
    {
        _muteButton.isOn = AudioManager.Instance.Muted;

        _masterSlider.value = AudioManager.Instance.GetVolume(SoundGroup.None);
        _musicSlider.value = AudioManager.Instance.GetVolume(SoundGroup.Music);
        _sfxSlider.value = AudioManager.Instance.GetVolume(SoundGroup.SFX);
        _uiSlider.value = AudioManager.Instance.GetVolume(SoundGroup.UI);
    }

    private void OnEnable()
    {
        _muteButton.onValueChanged.AddListener(Mute);

        _masterSlider.onValueChanged.AddListener(ChangeMasterVolume);
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
        _uiSlider.onValueChanged.AddListener(ChangeUIVolume);
    }

    private void OnDisable()
    {
        _muteButton.onValueChanged.RemoveAllListeners();

        _masterSlider.onValueChanged.RemoveAllListeners();
        _musicSlider.onValueChanged.RemoveAllListeners();
        _sfxSlider.onValueChanged.RemoveAllListeners();
        _uiSlider.onValueChanged.RemoveAllListeners();
    }

    private void Mute(bool muted)
    {
        if (muted)
        {
            AudioManager.Instance.MuteAll();
        }
        else
        {
            AudioManager.Instance.UnmuteAll();
        }
    }

    private void ChangeMasterVolume(float volume) => AudioManager.Instance.SetMasterVolume(volume);
    private void ChangeMusicVolume(float volume) => AudioManager.Instance.SetMusicVolume(volume);
    private void ChangeSFXVolume(float volume) => AudioManager.Instance.SetSFXVolume(volume);
    private void ChangeUIVolume(float volume) => AudioManager.Instance.SetUIVolume(volume);
}
