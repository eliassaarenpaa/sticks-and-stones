using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float spawnRateMin, spawnRateMax;
    [SerializeField] private int maxEnemies;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Transform> spawnPoints;

    private Transform _player;

    private float _spawnRateTimer;
    private float _currentSpawnRate;


    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    public void StartLevel()
    {
        _spawnRateTimer = 0;
        _currentSpawnRate = GetRandomSpawnRate();
    }

    private void Update()
    {
        _spawnRateTimer += Time.deltaTime;

        if(_spawnRateTimer >= _currentSpawnRate)
        {
            _currentSpawnRate = GetRandomSpawnRate();
            _spawnRateTimer = 0;

            var spawnPoint = GetSpawnPoint();
            var randomEnemyPrefab = GetRandomEnemy();
            var enemyInstance = Instantiate(randomEnemyPrefab);
            enemyInstance.transform.SetParent(transform);
            randomEnemyPrefab.transform.position = spawnPoint.position;
            enemyInstance.SetActive(true);
        }

    }

    private Transform GetSpawnPoint()
    {
        var spawnPointsAwayFromPlayer = spawnPoints.Where(x => Vector2.Distance(x.position, _player.position) > 2).ToList();
        var random = Random.Range(0, spawnPointsAwayFromPlayer.Count);
        return spawnPointsAwayFromPlayer[random];
    }

    private GameObject GetRandomEnemy()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }

    private float GetRandomSpawnRate()
    {
        return Random.Range(spawnRateMin, spawnRateMax);
    }

}
