using UnityEngine;

public class OptionsScreen : BaseScreen
{

    public void OpenMenu()
    {
        ScreenEvents.OnGameScreenOpenedInvoke( GameScreenType.MainMenu );
        Destroy( gameObject );
    }
}
