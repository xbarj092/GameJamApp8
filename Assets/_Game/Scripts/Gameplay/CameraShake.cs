using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public float _shakeDuration = 0.5f;
    public float _shakeIntensity = 0.3f;
    public float _decreaseFactor = 1.0f;

    private Vector3 _originalPosition;
    private float _currentShakeDuration = 0f;

    private Player _playerOne;
    private Player _playerTwo;

    private void Awake()
    {
        _playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        _playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
    }

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        _playerOne.OnDamageTaken += Shake;
        _playerTwo.OnDamageTaken += Shake;
    }

    private void OnDisable()
    {
        _playerOne.OnDamageTaken -= Shake;
        _playerTwo.OnDamageTaken -= Shake;
    }

    private void Update()
    {
        if (_currentShakeDuration > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * _shakeIntensity;
            shakeOffset.z = 0;

            transform.localPosition = new (_originalPosition.x + shakeOffset.x, _originalPosition.y + shakeOffset.y, transform.localPosition.z);

            _currentShakeDuration -= Time.deltaTime * _decreaseFactor;
        }
        else
        {
            _currentShakeDuration = 0f;
            transform.localPosition = new (_originalPosition.x, _originalPosition.y, transform.localPosition.z);
        }
    }

    public void Shake()
    {
        _currentShakeDuration = _shakeDuration;
    }

    public void Shake(float duration, float intensity)
    {
        _shakeDuration = duration;
        _shakeIntensity = intensity;
        _currentShakeDuration = duration;
    }

    public void ShakeCoroutine(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
