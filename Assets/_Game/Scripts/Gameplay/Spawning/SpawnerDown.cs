using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerDown : MonoBehaviour
{
    private float[] _positions = { -3.75f, -1.25f, 1.25f };
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _positionDelay = 2f;

    [Header("Obstacles")]
    [SerializeField] private List<Obstacle> _obstaclePrefabs;
    [SerializeField][Range(0f, 1f)] private float _spawnChance = 1f;

    [SerializeField] private SerializedDictionary<int, int> _spawnRateMultiplierThresholds = new();

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
        while (true)
        {
            foreach (KeyValuePair<int, int> threshold in _spawnRateMultiplierThresholds)
            {
                if (GameManager.Instance.SecondsPassed >= threshold.Key)
                {
                    _spawnInterval = Random.Range(1f, 3f) * threshold.Value;
                }
            }

            yield return new WaitForSeconds(_spawnInterval);

            if (Random.value <= _spawnChance)
            {
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
}
