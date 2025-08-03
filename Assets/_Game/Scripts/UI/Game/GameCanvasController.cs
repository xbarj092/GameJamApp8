using UnityEngine;

public class GameCanvasController : CanvasController
{
    [SerializeField] private HUD _HUDPrefab;
    [SerializeField] private ResultScreen _resultScreenPrefab;
    [SerializeField] private TutorialScreen _tutorialScreenPrefab;

    private void Awake()
    {
        InstantiateNewGameScreen(GameScreenType.HUD);

        if (!LocalDataStorage.Instance.PlayerPrefs.LoadTutorial())
        {
            InstantiateNewGameScreen(GameScreenType.Tutorial);
        }
    }

    protected override BaseScreen InstantiateNewGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.HUD => Instantiate(_HUDPrefab, transform),
            GameScreenType.Result => Instantiate(_resultScreenPrefab, transform),
            GameScreenType.Tutorial => Instantiate(_tutorialScreenPrefab, transform),
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
