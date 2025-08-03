using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject _visual;

    private Vector3 _rotationAngle;

    private void Start()
    {
        Quaternion randomRotation = Quaternion.Euler(
            Random.Range(0f, 360f),
            Random.Range(0f, 360f),
            Random.Range(0f, 360f)
        );

        _rotationAngle = new(UnityEngine.Random.Range(-0.5f, 0.5f),
            UnityEngine.Random.Range(-0.5f, 0.5f),
            UnityEngine.Random.Range(-0.5f, 0.5f));

        _visual.transform.rotation = randomRotation;

        Destroy(gameObject, 20f);
    }

    private void Update()
    {
        transform.Rotate(_rotationAngle);

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
