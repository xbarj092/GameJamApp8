using System.Collections;
using UnityEngine;

public class DamageBlink : MonoBehaviour
{

    public float blinkDuration = 1f;
    [SerializeField] private GameObject _srObject;
    private MeshRenderer _renderer;

    private Color _originalColor;
    private Player _player;

    private void OnEnable() => _player.OnDamageTaken += Flash;
    private void OnDisable() => _player.OnDamageTaken -= Flash;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _renderer = _srObject.GetComponent<MeshRenderer>();
        _originalColor = _renderer.material.color;
    }

    private void Flash()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {

        Color baseColor = _renderer.material.color;
        Color disabledColor = Color.white;
        for (int i = 0; i < 5; i++)
        {
            _renderer.material.color = disabledColor;
            yield return new WaitForSeconds(0.1f);
            _renderer.material.color = baseColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
