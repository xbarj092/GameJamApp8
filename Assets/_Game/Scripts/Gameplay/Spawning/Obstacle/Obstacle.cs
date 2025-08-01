using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject _visual;

    private void Start()
    {
        Quaternion randomRotation = Quaternion.Euler(
            Random.Range(0f, 360f),
            Random.Range(0f, 360f),
            Random.Range(0f, 360f)
        );

        _visual.transform.rotation = randomRotation;
    }

    private void Update()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Death();
            // AudioManager.Instance.Play(SoundType.PlayerHitObstacle);
        }

        if (other.CompareTag("ObjectDestroyer"))
        {
            Destroy(gameObject);
        }
    }
}
