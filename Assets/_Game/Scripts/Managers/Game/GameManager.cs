using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field: SerializeField] public float MovementSpeed {  get; private set; }

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
