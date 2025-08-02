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

        Destroy(gameObject, 20f);
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
            player.Damage(1f);
            // AudioManager.Instance.Play(SoundType.PlayerHitObstacle);
        }

        Destroy(gameObject);
    }
}
