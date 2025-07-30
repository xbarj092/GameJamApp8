using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    [HideInInspector] public bool IsBusy;

    private Dictionary<(SceneLoader.Scenes, SceneLoader.Scenes), Action> _transitionActions;

    protected override void Init()
    {
        base.Init();
        InitializeTransitionActions();
        GoBootToMenu();
    }

    private void InitializeTransitionActions()
    {
        _transitionActions = new Dictionary<(SceneLoader.Scenes, SceneLoader.Scenes), Action>
        {
            { (SceneLoader.Scenes.BootScene, SceneLoader.Scenes.MenuScene), () => { } },
            { (SceneLoader.Scenes.MenuScene, SceneLoader.Scenes.LevelOneScene), () => { } },
            { (SceneLoader.Scenes.MenuScene, SceneLoader.Scenes.LevelTwoScene), () => { } },
            { (SceneLoader.Scenes.MenuScene, SceneLoader.Scenes.LevelThreeScene), () => { } },
            { (SceneLoader.Scenes.LevelOneScene, SceneLoader.Scenes.MenuScene), () => { } },
            { (SceneLoader.Scenes.LevelTwoScene, SceneLoader.Scenes.MenuScene), () => { } },
            { (SceneLoader.Scenes.LevelThreeScene, SceneLoader.Scenes.MenuScene), () => { } },
            { (SceneLoader.Scenes.LevelOneScene, SceneLoader.Scenes.LevelOneScene), () => { } },
            { (SceneLoader.Scenes.LevelTwoScene, SceneLoader.Scenes.LevelTwoScene), () => { } },
            { (SceneLoader.Scenes.LevelThreeScene, SceneLoader.Scenes.LevelThreeScene), () => { } },
        };
    }

    public void GoBootToMenu() => LoadSceneWithTransition(SceneLoader.Scenes.BootScene, SceneLoader.Scenes.MenuScene);
    public void GoMenuToLevelOne() => LoadSceneWithTransition(SceneLoader.Scenes.MenuScene, SceneLoader.Scenes.LevelOneScene);
    public void GoMenuToLevelTwo() => LoadSceneWithTransition(SceneLoader.Scenes.MenuScene, SceneLoader.Scenes.LevelTwoScene);
    public void GoMenuToLevelThree() => LoadSceneWithTransition(SceneLoader.Scenes.MenuScene, SceneLoader.Scenes.LevelThreeScene);
    public void GoLevelOneToMenu() => LoadSceneWithTransition(SceneLoader.Scenes.LevelOneScene, SceneLoader.Scenes.MenuScene);
    public void GoLevelTwoToMenu() => LoadSceneWithTransition(SceneLoader.Scenes.LevelTwoScene, SceneLoader.Scenes.MenuScene);
    public void GoLevelThreeToMenu() => LoadSceneWithTransition(SceneLoader.Scenes.LevelThreeScene, SceneLoader.Scenes.MenuScene);
    public void RestartLevelOne() => LoadSceneWithTransition(SceneLoader.Scenes.LevelOneScene, SceneLoader.Scenes.LevelOneScene);
    public void RestartLevelTwo() => LoadSceneWithTransition(SceneLoader.Scenes.LevelTwoScene, SceneLoader.Scenes.LevelTwoScene);
    public void RestartLevelThree() => LoadSceneWithTransition(SceneLoader.Scenes.LevelThreeScene, SceneLoader.Scenes.LevelThreeScene);

    private void LoadSceneWithTransition(SceneLoader.Scenes fromScene, SceneLoader.Scenes toScene)
    {
        if (!IsSceneLoaded(fromScene) || IsBusy)
        {
            Debug.Log("[GPM] - didnt load back to the lobby scene");
            return;
        }

        IsBusy = true;
        (SceneLoader.Scenes, SceneLoader.Scenes) transitionKey = (fromScene, toScene);
        void OnSceneLoadComplete(SceneLoader.Scenes loadedScene)
        {
            if (_transitionActions.TryGetValue(transitionKey, out Action transitionAction))
            {
                transitionAction?.Invoke();
            }
            else
            {
                Time.timeScale = 1;
            }

            IsBusy = false;
            SceneLoader.OnSceneLoadDone -= OnSceneLoadComplete;
        }

        SceneLoader.OnSceneLoadDone += OnSceneLoadComplete;
        SceneLoader.LoadScene(toScene, toUnload: fromScene);
    }

    public bool IsSceneLoaded(SceneLoader.Scenes sceneToCheck) => SceneLoader.IsSceneLoaded(sceneToCheck);
}
