using TMPro;
using UnityEngine;

public class HUD : BaseScreen
{
    [SerializeField] private OptionsPopup _optionsPopup;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timeToSwapText;

    private int _currentSeconds = 30;

    private void Start()
    {
        InvokeRepeating(nameof(DecrementSeconds), 0f, 1f);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayersSwapped += ResetSwapTimer;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayersSwapped += ResetSwapTimer;
    }

    private void DecrementSeconds()
    {
        _currentSeconds--;
    }

    private void ResetSwapTimer()
    {
        _currentSeconds = GameManager.Instance.SecondsToSwapPlayers;
    }

    private void Update()
    {
        _scoreText.text = GameManager.Instance.Score.ToString();
        _timeToSwapText.text = _currentSeconds.ToString();
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
