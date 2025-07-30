using UnityEngine;

public class MenuCanvasController : CanvasController
{
    [SerializeField] private MainMenuScreen _mainMenuScreenPrefab;

    private void Awake()
    {
        InstantiateNewGameScreen(GameScreenType.MainMenu);
    }

    protected override BaseScreen InstantiateNewGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.MainMenu => Instantiate(_mainMenuScreenPrefab, transform),
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
