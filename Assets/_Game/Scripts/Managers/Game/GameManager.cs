using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private float _baseMovementSpeed;
    [SerializeField] private AnimationCurve _movementSpeedCurve;
    public float MovementSpeed
    {
        get => _movementSpeedCurve.Evaluate((float)SecondsPassed / 60f) * _baseMovementSpeed;
    }

    public int SecondsToSwapPlayers = 30;

    public int SecondsPassed = 0;
    public int Score;

    public bool CanPlay = false;

    public int CurrentPlayerIndex = 0;

    public event Action OnPlayersSwapped;

    private void Start()
    {
        InvokeRepeating(nameof(IncrementSeconds), 0f, 1f);
    }

    private void FixedUpdate()
    {
        if (!CanPlay)
        {
            return;
        }

        if (!SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene))
        {
            return;
        }

        Score++;
    }

    private void IncrementSeconds()
    {
        if (!SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene))
        {
            return;
        }

        SecondsPassed++;

        if (SecondsPassed % SecondsToSwapPlayers == 0)
        {
            OnPlayersSwapped?.Invoke();
            CurrentPlayerIndex = CurrentPlayerIndex == 0 ? 1 : 0;
        }
    }

    public void Restart()
    {
        SecondsPassed = 0;
        Score = 0;
        CurrentPlayerIndex = 0;
    }
}
