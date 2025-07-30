using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    protected Dictionary<GameScreenType, BaseScreen> _instantiatedScreens = new();

    protected virtual void OnEnable()
    {
        ScreenEvents.OnGameScreenOpened += ShowGameScreen;
        ScreenEvents.OnGameScreenClosed += CloseGameScreen;
    }

    protected virtual void OnDisable()
    {
        ScreenEvents.OnGameScreenOpened -= ShowGameScreen;
        ScreenEvents.OnGameScreenClosed -= CloseGameScreen;
    }

    protected void ShowGameScreen(GameScreenType gameScreenType)
    {
        if ((_instantiatedScreens.ContainsKey(gameScreenType) && _instantiatedScreens[gameScreenType] == null) ||
            !_instantiatedScreens.ContainsKey(gameScreenType))
        {
            InstantiateScreenFromScreenType(gameScreenType);
        }

        _instantiatedScreens[gameScreenType].Open();
    }

    protected void CloseGameScreen(GameScreenType gameScreenType)
    {
        if (_instantiatedScreens.ContainsKey(gameScreenType))
        {
            GameScreenType nextScreenType = GetPreviousScreen(gameScreenType);
            if (nextScreenType != GameScreenType.None)
            {
                if (_instantiatedScreens.TryGetValue(nextScreenType, out BaseScreen existingScreen) && existingScreen != null)
                {
                    existingScreen.Open();
                }
                else
                {
                    InstantiateScreenFromScreenType(nextScreenType);
                }
            }

            Destroy(_instantiatedScreens[gameScreenType].gameObject);
            _instantiatedScreens.Remove(gameScreenType);
        }
    }

    protected void DestroyGameScreen(GameScreenType gameScreenType)
    {
        if (_instantiatedScreens.ContainsKey(gameScreenType))
        {
            Destroy(_instantiatedScreens[gameScreenType].gameObject);
            _instantiatedScreens.Remove(gameScreenType);
        }
    }

    private void InstantiateScreenFromScreenType(GameScreenType gameScreenType)
    {
        BaseScreen screenInstance = InstantiateNewGameScreen(gameScreenType);
        InstantiateScreen(screenInstance);
    }

    private void InstantiateScreen(BaseScreen screenInstance)
    {
        if (screenInstance != null)
        {
            _instantiatedScreens[screenInstance.GameScreenType] = screenInstance;
            ScreenManager.Instance.ActiveGameScreen = screenInstance;
        }
    }

    protected virtual BaseScreen InstantiateNewGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            _ => null
        };
    }

    protected virtual GameScreenType GetPreviousScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            _ => GameScreenType.None
        };
    }
}
