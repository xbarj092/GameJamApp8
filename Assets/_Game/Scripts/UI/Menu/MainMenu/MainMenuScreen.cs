using UnityEngine;

public class MainMenuScreen : BaseScreen
{
    [SerializeField] private OptionsPopup _optionsPopup;

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