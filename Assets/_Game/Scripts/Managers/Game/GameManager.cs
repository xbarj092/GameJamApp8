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

    public int CurrentPlayerIndex = 0;

    public event Action OnPlayersSwapped;

    private void Start()
    {
        InvokeRepeating(nameof(IncrementSeconds), 0f, 1f);
    }

    private void Update()
    {
        Score++;
    }

    private void IncrementSeconds()
    {
        SecondsPassed++;

        if (SecondsPassed % SecondsToSwapPlayers == 0)
        {
            OnPlayersSwapped?.Invoke();
            CurrentPlayerIndex = CurrentPlayerIndex == 0 ? 1 : 0;
        }
    }
}
