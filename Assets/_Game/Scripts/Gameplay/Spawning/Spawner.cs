using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float[] _positions = { -3.75f, -1.25f, 1.25f };

    [Header("Obstacles")]
    [SerializeField] private List<Obstacle> _obstaclePrefabs;
    [SerializeField][Range(0f, 1f)] private float _spawnChance = 1f;
    [SerializeField] private AnimationCurve _spawnInterval;

    [Header("Pickupables")]
    [SerializeField] private PickupableItem _shieldPickupPrefab;
    [SerializeField] private PickupableItem _hpPickupPrefab;
    [SerializeField] private PickupableItem _bulletPickupPrefab;

    [SerializeField][Range(0f, 1f)] private float _pickupSpawnChance = 0.0f;

    private Player _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval.Evaluate((float)GameManager.Instance.SecondsPassed / 60f));
            if (Random.value <= _spawnChance)
            {
                if (Random.value <= _pickupSpawnChance)
                {
                    SpawnRandomPickup();
                }
                else
                {
                    SpawnObstacle();
                }
            }
        }
    }

    private void SpawnObstacle()
    {
        float spawnPositionX = _positions[Random.Range(0, _positions.Length)];
        Vector3 spawnPosition = new(spawnPositionX, transform.position.y, transform.position.z);

        Obstacle obstacle = _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)];

        Obstacle spawnedObstacle = Instantiate(obstacle, spawnPosition, Quaternion.identity);
        spawnedObstacle.transform.localScale = Vector3.one;
        Vector2 obstacleSize = Vector3.one;
        Collider2D[] collidersInRange = Physics2D.OverlapBoxAll(spawnPosition, obstacleSize, 0f);
        bool hasOverlapWithOtherObstacles = collidersInRange.Where(collider => collider.CompareTag("Obstacle") && 
            !collider.transform.IsChildOf(spawnedObstacle.transform)).Any();
        if (hasOverlapWithOtherObstacles)
        {
            Destroy(spawnedObstacle.gameObject);
        }
    }

    private void SpawnRandomPickup()
    {
        float hpWeight = 1f;
        /*float ammoWeight = 1f;
        float shieldWeight = 1f;*/

        if (_player.CurrentHealth() <= 50)
        {
            hpWeight = 2f;
        }
        /*if (_player.Ammo <= 5)
        {
            ammoWeight = 4f;
        }
        if (!_player.Shield)
        {
            shieldWeight = 1.5f;
        }*/

        float totalWeight = hpWeight;// + ammoWeight + shieldWeight;
        float randomValue = Random.Range(0f, totalWeight);

        if (randomValue < hpWeight)
        {
            SpawnPickup(_hpPickupPrefab, new HpPickup(1));
        }
        /*else if (randomValue < hpWeight + ammoWeight)
        {
            SpawnPickup(_bulletPickupPrefab, new BulletPickup(5));
        }
        else
        {
            SpawnPickup(_shieldPickupPrefab, new ShieldPickup());
        }*/
    }

    private void SpawnPickup(PickupableItem prefab, IPickupable pickupStrategy)
    {
        float spawnPositionX = _positions[Random.Range(0, _positions.Length)];
        Vector3 spawnPosition = new(spawnPositionX, transform.position.y, transform.position.z);
        PickupableItem pickup = Instantiate(prefab, spawnPosition, Quaternion.identity);
        pickup.SetPickupable(pickupStrategy);
    }
}
