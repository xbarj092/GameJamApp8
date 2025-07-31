public class LevelSelectScreen : BaseScreen
{
    public void OpenLevelOne() => SceneLoadManager.Instance.GoMenuToLevelOne();
    public void OpenLevelTwo() => SceneLoadManager.Instance.GoMenuToLevelTwo();
    public void OpenLevelThree() => SceneLoadManager.Instance.GoMenuToLevelThree();

    public void OpenMenu()
    {
        ScreenEvents.OnGameScreenOpenedInvoke( GameScreenType.MainMenu );
        Destroy( gameObject );
    }
}
