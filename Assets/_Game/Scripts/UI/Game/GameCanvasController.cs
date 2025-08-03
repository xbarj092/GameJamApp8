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
            FindFirstObjectByType<HUD>().SetButtonInteractable(false);
            InstantiateNewGameScreen(GameScreenType.Tutorial);
            GameManager.Instance.CanPlay = false;
            GameManager.Instance.Score = 0;
        }
        else
        {
            GameManager.Instance.CanPlay = true;
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
