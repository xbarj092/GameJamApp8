using DG.Tweening.Core.Easing;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageFX : MonoBehaviour
{

    [SerializeField] private ParticleSystem _damageFXPrefab;
    private Player _player;

    private void OnEnable() => _player.OnDamageTaken += InstantiateFX;
    private void OnDisable() => _player.OnDamageTaken -= InstantiateFX;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void InstantiateFX()
    {
        if (_damageFXPrefab != null)
        {
            Instantiate(_damageFXPrefab, _player.transform.position, Quaternion.identity);
        }
    }
}
