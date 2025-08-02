using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private float _baseMovementSpeed;
    [SerializeField] private AnimationCurve _movementSpeedCurve;
    public float MovementSpeed
    {
        get => _movementSpeedCurve.Evaluate((float)SecondsPassed / 60f) * _baseMovementSpeed;
    }

    public int SecondsPassed = 0;
    public int Score;

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
    }
}
