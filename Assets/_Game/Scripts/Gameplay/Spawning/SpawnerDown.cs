using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerDown : MonoBehaviour
{
    private float[] _positions = { -3.75f, -1.25f, 1.25f };
    [SerializeField] private AnimationCurve _spawnInterval;
    [SerializeField] private float _positionDelay = 2f;

    [Header("Obstacles")]
    [SerializeField] private List<Obstacle> _obstaclePrefabs;
    [SerializeField][Range(0f, 1f)] private float _spawnChance = 1f;

    [Header("Pickupables")]
    [SerializeField] private PickupableItem _shieldPickupPrefab;
    [SerializeField] private PickupableItem _hpPickupPrefab;
    [SerializeField] private PickupableItem _bulletPickupPrefab;

    [SerializeField][Range(0f, 1f)] private float _pickupSpawnChance = 0.0f;

    private Player _player;
    private Queue<int> _playerPositionHistory = new();
    private float _positionTrackingInterval = 0.1f;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
    }

    private void Start()
    {
        StartCoroutine(TrackPlayerPosition());
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator TrackPlayerPosition()
    {
        while (true)
        {
            _playerPositionHistory.Enqueue(_player.CurrentLine);

            int maxPositions = Mathf.CeilToInt(_positionDelay / _positionTrackingInterval) + 5;
            while (_playerPositionHistory.Count > maxPositions)
            {
                _playerPositionHistory.Dequeue();
            }

            yield return new WaitForSeconds(_positionTrackingInterval);
        }
    }

    private float GetPlayerPositionFromPast()
    {
        return _playerPositionHistory.Count > 0 ? _positions[_playerPositionHistory.Peek()] : _positions[_player.CurrentLine];
    }

    private List<float> GetSpawnPoints(float targetX)
    {
        List<float> spawnPoints = new();

        foreach (float position in _positions)
        {
            if (Mathf.Abs(targetX - position) > 0.01f)
            {
                spawnPoints.Add(position);
            }
        }

        return spawnPoints;
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval.Evaluate((float)GameManager.Instance.SecondsPassed / 60f));

            if (Random.value <= _spawnChance)
            {
                if (Random.value <= _pickupSpawnChance)
                {
                    SpawnPickupable();
                }
                SpawnObstacle();
            }
        }
    }

    private void SpawnObstacle()
    {
        float pastPlayerPosition = GetPlayerPositionFromPast();

        List<float> targetSpawnXList = GetSpawnPoints(pastPlayerPosition);

        foreach (float targetSpawnX in targetSpawnXList)
        {
            Vector3 spawnPosition = new(targetSpawnX, transform.position.y, transform.position.z);

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
    }

    private void SpawnPickupable()
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

        if (/*randomValue < hpWeight*/ true)
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
        Vector3 spawnPosition = new(GetPlayerPositionFromPast(), transform.position.y, transform.position.z);
        PickupableItem pickup = Instantiate(prefab, spawnPosition, Quaternion.identity);
        pickup.SetPickupable(pickupStrategy);
    }
}
