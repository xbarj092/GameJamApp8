using UnityEngine;

public class SoundPlayerUI : MonoBehaviour
{
    [SerializeField] private SoundType _soundType;

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound(_soundType);
    }
}
