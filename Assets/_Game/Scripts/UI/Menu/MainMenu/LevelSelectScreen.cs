using UnityEngine;

public class LevelSelectScreen : BaseScreen
{

    SceneLoadManager _sceneLoadManager = SceneLoadManager.Instance;

    public void OpenLevelOne() => _sceneLoadManager.GoMenuToLevelOne();
    public void OpenLevelTwo() => _sceneLoadManager.GoMenuToLevelTwo();
    public void OpenLevelThree() => _sceneLoadManager.GoMenuToLevelThree();

    public void OpenMenu()
    {
        ScreenEvents.OnGameScreenOpenedInvoke( GameScreenType.MainMenu );
        Destroy( gameObject );
    }
}
