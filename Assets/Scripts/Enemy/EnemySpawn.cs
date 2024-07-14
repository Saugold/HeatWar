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
        public int spawnLimit; // Limite máximo de spawn para este tipo de inimigo
        [HideInInspector]
        public int currentSpawnCount = 0; // Contador de inimigos spawnados
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
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        float totalProbability = 0;

        // Calcular a probabilidade total considerando apenas os tipos que ainda não atingiram seu limite de spawn
        foreach (var enemyType in enemyTypes)
        {
            if (enemyType.currentSpawnCount < enemyType.spawnLimit)
            {
                totalProbability += enemyType.spawnProbability;
            }
        }

        if (totalProbability == 0) // Todos os tipos atingiram seu limite de spawn
        {
            return;
        }

        float randomValue = Random.Range(0, totalProbability);

        float cumulativeProbability = 0;
        foreach (var enemyType in enemyTypes)
        {
            if (enemyType.currentSpawnCount < enemyType.spawnLimit)
            {
                cumulativeProbability += enemyType.spawnProbability;
                if (randomValue <= cumulativeProbability)
                {
                    Instantiate(enemyType.enemyPrefab, transform.position, Quaternion.identity);
                    enemyType.currentSpawnCount++;
                    break;
                }
            }
        }
    }
}
