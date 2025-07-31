using UnityEngine;

public class MenuCanvasController : CanvasController
{
    [SerializeField] private MainMenuScreen _mainMenuScreenPrefab;
    [SerializeField] private LevelSelectScreen _levelSelectScreenPrefab;

    private void Awake()
    {
        InstantiateNewGameScreen(GameScreenType.MainMenu);
    }

    protected override BaseScreen InstantiateNewGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.MainMenu => Instantiate(_mainMenuScreenPrefab, transform),
            GameScreenType.LevelSelect => Instantiate(_levelSelectScreenPrefab, transform),
            _ => base.InstantiateNewGameScreen(gameScreenType),
        };
    }

    protected override GameScreenType GetPreviousScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            _ => base.GetPreviousScreen(gameScreenType),
        };
    }
}
