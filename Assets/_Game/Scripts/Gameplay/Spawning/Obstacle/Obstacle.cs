using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(GameManager.Instance.MovementSpeed * Time.deltaTime * Vector3.back);
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
    }
}
