using UnityEngine;

public class DamageMenuFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _damageFXPrefab;
    private MenuObject _menuObject;

    private void OnEnable() => _menuObject.Ondestroyed += InstantiateFX;
    private void OnDisable() => _menuObject.Ondestroyed -= InstantiateFX;

    private void Awake() => _menuObject = GetComponent<MenuObject>();

    private void InstantiateFX()
    {
        if (_damageFXPrefab != null)
        {
            Instantiate(_damageFXPrefab, _menuObject.transform.position, Quaternion.identity);
            _damageFXPrefab.transform.localScale = Vector3.one * Random.Range(0.1f, 0.3f);
        }
        AudioManager.Instance.PlaySound(SoundType.SpaceshipCrash);
    }
}
