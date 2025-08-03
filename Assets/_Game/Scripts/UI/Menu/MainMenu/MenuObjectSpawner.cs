using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjectSpawner : MonoBehaviour
{
    public List<MenuObject> objectPrefabs;
    public float spawnInterval = 2f;
    private Camera _mainCamera;

    private int _lastSide;

    void Start()
    {
        _mainCamera = Camera.main;
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObject()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        Vector2 destination = GetDiagonalOppositePosition(spawnPosition);
        MenuObject selectedObject = objectPrefabs[Random.Range(0, objectPrefabs.Count)];
        MenuObject spawnedObject = Instantiate(selectedObject, spawnPosition, Quaternion.identity);
        spawnedObject.SetDestination(destination);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float zDistance = 10f;

        Vector2 screenMin = _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, zDistance));
        Vector2 screenMax = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, zDistance));

        int edge = 0;
        do
        {
            edge = Random.Range(0, 4);
        }
        while(edge == _lastSide);

        Vector2 spawnPos = Vector2.zero;

        _lastSide = edge;

        switch (edge)
        {
            case 0: // Left edge
                spawnPos = new Vector2(screenMin.x - 1, Random.Range(screenMin.y, screenMax.y));
                break;
            case 1: // Right edge
                spawnPos = new Vector2(screenMax.x + 1, Random.Range(screenMin.y, screenMax.y));
                break;
            case 2: // Top edge
                spawnPos = new Vector2(Random.Range(screenMin.x, screenMax.x), screenMax.y + 1);
                break;
            case 3: // Bottom edge
                spawnPos = new Vector2(Random.Range(screenMin.x, screenMax.x), screenMin.y - 1);
                break;
        }
        return spawnPos;
    }

    private Vector2 GetDiagonalOppositePosition(Vector2 spawnPosition)
    {
        float zDistance = 10f;

        Vector2 screenMin = _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, zDistance));
        Vector2 screenMax = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, zDistance));

        Vector2 destination = Vector2.zero;
        destination.x = (spawnPosition.x < screenMin.x) ? screenMax.x + 20 : screenMin.x - 20;
        destination.y = (spawnPosition.y < screenMin.y) ? screenMax.y + 20 : screenMin.y - 20;

        return destination;
    }
}
