using UnityEngine;

public class MainMenuScreen : BaseScreen
{
    public void OpenLevelSelect()
    {
        ScreenEvents.OnGameScreenOpenedInvoke( GameScreenType.LevelSelect );
        Destroy( gameObject );
    }

    public void OpenOptions()
    {
        ScreenEvents.OnGameScreenOpenedInvoke( GameScreenType.Options );
        Destroy( gameObject );
    }

    public void ExitGame() => Application.Quit();
}