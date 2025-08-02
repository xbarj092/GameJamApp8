using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Player _playerUp;
    private Player _playerDown;

    [SerializeField] private Image _upperHealthUIFillBar;
    [SerializeField] private Image _lowerHealthUIFillBar;

    private readonly int[] _upperHealthWidths = { 0, 27, 47, 66 };
    private readonly int[] _lowerHealthWidths = { 0, 17, 30, 42 };

    private void OnEnable()
    {
        _playerUp = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        _playerDown = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();

        if ( _playerUp != null ) _playerUp.OnHealthChanged += UpdateUpperHealthUI;
        if ( _playerDown != null ) _playerDown.OnHealthChanged += UpdateLowerHealthUI;
    }

    private void UpdateUpperHealthUI(float value)
    {
        Vector2 size = _upperHealthUIFillBar.rectTransform.sizeDelta;
        size.x = _upperHealthWidths[(int)value];
        _upperHealthUIFillBar.rectTransform.sizeDelta = size;
    }

    private void UpdateLowerHealthUI(float value)
    {
        Vector2 size = _lowerHealthUIFillBar.rectTransform.sizeDelta;
        size.x = _lowerHealthWidths[(int)value];
        _lowerHealthUIFillBar.rectTransform.sizeDelta = size;
    }

    private void OnDisable()
    {
        if (_playerUp != null) _playerUp.OnHealthChanged -= UpdateUpperHealthUI;
        if (_playerDown != null) _playerDown.OnHealthChanged -= UpdateLowerHealthUI;

        _playerUp = null;
        _playerDown = null;
    }
}
