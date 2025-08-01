using System.Collections.Generic;
using UnityEngine;

public class SimpleChunkSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _chunkPrefabs;
    [SerializeField] private Transform _player;
    [SerializeField] private float _chunkSize = 100f;
    [SerializeField] private int _chunksToKeep = 4;

    private Dictionary<int, GameObject> _activeChunks = new();
    private int _currentChunkIndex = 0;

    private void Start()
    {
        for (int i = -1; i <= 1; i++)
        {
            SpawnChunk(i);
        }
    }

    private void Update()
    {
        int playerChunkIndex = Mathf.CeilToInt(_player.position.z / _chunkSize);

        if (playerChunkIndex + 1 != _currentChunkIndex)
        {
            _currentChunkIndex = playerChunkIndex;
            UpdateChunks();
        }
    }

    private void UpdateChunks()
    {
        for (int i = -1; i <= 1; i++)
        {
            int chunkIndex = _currentChunkIndex + i;
            if (!_activeChunks.ContainsKey(chunkIndex))
            {
                SpawnChunk(chunkIndex);
            }
        }

        List<int> chunksToRemove = new List<int>();
        foreach (KeyValuePair<int, GameObject> kvp in _activeChunks)
        {
            int chunkIndex = kvp.Key;
            if (Mathf.Abs(chunkIndex - _currentChunkIndex) > 1)
            {
                chunksToRemove.Add(chunkIndex);
            }
        }

        foreach (int index in chunksToRemove)
        {
            if (_activeChunks[index] != null)
            {
                Destroy(_activeChunks[index]);
            }
            _activeChunks.Remove(index);
        }
    }

    private void SpawnChunk(int chunkIndex)
    {
        GameObject prefab = _chunkPrefabs[Random.Range(0, _chunkPrefabs.Count)];
        Vector3 position = new(0, 0, chunkIndex * _chunkSize);

        GameObject chunk = Instantiate(prefab, position, Quaternion.identity);
        chunk.name = $"Chunk_{chunkIndex}";

        _activeChunks[chunkIndex] = chunk;
    }
}
