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
        GoBootToSignIn();
    }

    private void InitializeTransitionActions()
    {
        _transitionActions = new Dictionary<(SceneLoader.Scenes, SceneLoader.Scenes), Action>
        {
            { (SceneLoader.Scenes.BootScene, SceneLoader.Scenes.MenuScene), () => { } },
            { (SceneLoader.Scenes.MenuScene, SceneLoader.Scenes.GameScene), () => { } },
            { (SceneLoader.Scenes.GameScene, SceneLoader.Scenes.MenuScene), () => { } },
            { (SceneLoader.Scenes.GameScene, SceneLoader.Scenes.GameScene), () => { } },
        };
    }

    public void GoBootToSignIn() => LoadSceneWithTransition(SceneLoader.Scenes.BootScene, SceneLoader.Scenes.MenuScene);
    public void GoMenuToGame() => LoadSceneWithTransition(SceneLoader.Scenes.MenuScene, SceneLoader.Scenes.GameScene);
    public void GoGameToMenu() => LoadSceneWithTransition(SceneLoader.Scenes.GameScene, SceneLoader.Scenes.MenuScene);
    public void RestartGame() => LoadSceneWithTransition(SceneLoader.Scenes.GameScene, SceneLoader.Scenes.GameScene);

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
