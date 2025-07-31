using UnityEngine;

public class GameCanvasController : CanvasController
{
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _optionsScreen;

    public void OpenOptions()
    {
        Time.timeScale = 0f;
        _gameScreen.SetActive( false );
        _optionsScreen.SetActive( true );
    }

    public void CloseOptions()
    {
        _gameScreen.SetActive( true );
        _optionsScreen.SetActive( false );
        Time.timeScale = 1f;
    }
}
