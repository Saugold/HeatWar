using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public float spawnProbability;
    }

    [SerializeField] private EnemySpawnInfo[] enemyTypes;
    [SerializeField] private float spawnInterval = 2f; // Tempo entre os spawns

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float totalProbability = 0;

        foreach (var enemyType in enemyTypes)
        {
            totalProbability += enemyType.spawnProbability;
        }

        float randomValue = Random.Range(0, totalProbability);

        float cumulativeProbability = 0;
        foreach (var enemyType in enemyTypes)
        {
            cumulativeProbability += enemyType.spawnProbability;
            if (randomValue <= cumulativeProbability)
            {
                Instantiate(enemyType.enemyPrefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
