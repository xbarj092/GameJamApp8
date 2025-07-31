using UnityEngine;

public class GameCanvasController : CanvasController
{
    [SerializeField] private HUD _HUDPrefab;
    [SerializeField] private ResultScreen _resultScreenPrefab;
    [SerializeField] private ThankYouScreen _thankYouScreenPrefab;

    private void Awake()
    {
        InstantiateNewGameScreen(GameScreenType.HUD);
    }

    protected override BaseScreen InstantiateNewGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.HUD => Instantiate(_HUDPrefab, transform),
            GameScreenType.Result => Instantiate(_resultScreenPrefab, transform),
            GameScreenType.ThankYouForPlaying => Instantiate(_thankYouScreenPrefab, transform),
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
