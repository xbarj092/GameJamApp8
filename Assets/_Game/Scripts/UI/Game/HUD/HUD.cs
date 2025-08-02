using System;
using TMPro;
using UnityEngine;

public class HUD : BaseScreen
{
    [SerializeField] private OptionsPopup _optionsPopup;

    [SerializeField] private TMP_Text _scoreText;

    private void Update()
    {
        _scoreText.text = GameManager.Instance.Score.ToString();
    }

    public void OpenOptions()
    {
        Time.timeScale = 0f;
        _optionsPopup.gameObject.SetActive(true);
    }

    public void CloseOptions()
    {
        _optionsPopup.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
