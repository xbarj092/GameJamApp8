using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : BaseScreen
{
    [SerializeField] private OptionsPopup _optionsPopup;
    [SerializeField] private Image _settingsImage;

    [SerializeField] private Button _settingsButton;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timeToSwapText;

    [Header("Health animation")]
    [SerializeField] private RectTransform _diamondTransform;
    [SerializeField] private Vector3 _diamondPlayerOneRotation;
    [SerializeField] private Vector3 _diamondPlayerTwoRotation;
    [SerializeField] private float _diamondRotationDuration;

    [SerializeField] private RectTransform _playerOneBarTransform;
    [SerializeField] private Vector3 _playerOneBarPlayerOneRotation;
    [SerializeField] private Vector3 _playerOneBarPlayerTwoRotation;
    [SerializeField] private Vector3 _playerOneBarPlayerOnePosition;
    [SerializeField] private Vector3 _playerOneBarPlayerTwoPosition;
    [SerializeField] private Vector3 _playerOneBarPlayerOneScale;
    [SerializeField] private Vector3 _playerOneBarPlayerTwoScale;
    [SerializeField] private float _playerOneBarRotationDuration;
    [SerializeField] private float _playerOneBarPositionDuration;
    [SerializeField] private float _playerOneBarScaleDuration;

    [SerializeField] private RectTransform _playerTwoBarTransform;
    [SerializeField] private Vector3 _playerTwoBarPlayerOneRotation;
    [SerializeField] private Vector3 _playerTwoBarPlayerTwoRotation;
    [SerializeField] private Vector3 _playerTwoBarPlayerOnePosition;
    [SerializeField] private Vector3 _playerTwoBarPlayerTwoPosition;
    [SerializeField] private Vector3 _playerTwoBarPlayerOneScale;
    [SerializeField] private Vector3 _playerTwoBarPlayerTwoScale;
    [SerializeField] private float _playerTwoBarRotationDuration;
    [SerializeField] private float _playerTwoBarPositionDuration;
    [SerializeField] private float _playerTwoBarScaleDuration;

    private int _currentSeconds = 30;

    private void Start()
    {
        SetButtonInteractable(LocalDataStorage.Instance.PlayerPrefs.LoadTutorial());
        InvokeRepeating(nameof(DecrementSeconds), 0f, 1f);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayersSwapped += SwapPlayers;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayersSwapped -= SwapPlayers;
    }

    private void DecrementSeconds()
    {
        _currentSeconds--;
    }

    private void SwapPlayers()
    {
        _currentSeconds = GameManager.Instance.SecondsToSwapPlayers;

        bool playerOne = GameManager.Instance.CurrentPlayerIndex == 1;

        // diamond
        _diamondTransform.DORotate(playerOne ? _diamondPlayerOneRotation : _diamondPlayerTwoRotation, _diamondRotationDuration).SetEase(Ease.OutBack);

        // player one bar
        _playerOneBarTransform.DOLocalRotate(playerOne ? _playerOneBarPlayerOneRotation : _playerOneBarPlayerTwoRotation, _playerOneBarRotationDuration).SetEase(Ease.OutBack);
        _playerOneBarTransform.DOLocalMove(playerOne ? _playerOneBarPlayerOnePosition : _playerOneBarPlayerTwoPosition, _playerOneBarPositionDuration).SetEase(Ease.OutBack);
        _playerOneBarTransform.DOScale(playerOne ? _playerOneBarPlayerOneScale : _playerOneBarPlayerTwoScale, _playerOneBarScaleDuration).SetEase(Ease.OutBack);

        // player two bar
        _playerTwoBarTransform.DOLocalRotate(playerOne ? _playerTwoBarPlayerOneRotation : _playerTwoBarPlayerTwoRotation, _playerTwoBarRotationDuration).SetEase(Ease.OutBack);
        _playerTwoBarTransform.DOLocalMove(playerOne ? _playerTwoBarPlayerOnePosition : _playerTwoBarPlayerTwoPosition, _playerTwoBarPositionDuration).SetEase(Ease.OutBack);
        _playerTwoBarTransform.DOScale(playerOne ? _playerTwoBarPlayerOneScale : _playerTwoBarPlayerTwoScale, _playerTwoBarScaleDuration).SetEase(Ease.OutBack);

        _settingsImage.color = playerOne ? Color.white : Color.black;
    }

    private void Update()
    {
        _scoreText.text = GameManager.Instance.Score.ToString();
        _timeToSwapText.text = _currentSeconds.ToString();
    }

    public void SetButtonInteractable(bool buttonInteractable)
    {
        _settingsButton.interactable = buttonInteractable;
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

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.Restart();
        SceneLoadManager.Instance.GoGameToMenu();
    }
}
