using DG.Tweening;
using UnityEngine;

public class MainMenuScreen : BaseScreen
{
    [SerializeField] private OptionsPopup _optionsPopup;
    private int _seconds = 10;

    private void Awake()
    {
        InvokeRepeating(nameof(IncrementSeconds), 0f, 1f);
    }

    private void IncrementSeconds()
    {
        _seconds--;
        if (_seconds % 10 == 0)
        {
            _seconds = 10;
            Camera.main.transform.DORotate(new Vector3(0, 0, Camera.main.transform.rotation.z == 0 ? 180 : 0), 0.5f).SetEase(Ease.OutBack);
            transform.DORotate(new Vector3(0, 0, transform.rotation.z == 0 ? 180 : 0), 0.5f).SetEase(Ease.OutBack);
        }
    }

    public void Play()
    {
        SceneLoadManager.Instance.GoMenuToGame();
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

    public void ExitGame() => Application.Quit();
}
