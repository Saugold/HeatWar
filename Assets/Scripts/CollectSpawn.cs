using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSpawn : MonoBehaviour
{
    [System.Serializable]
    public class CollectibleSpawnInfo
    {
        public GameObject collectiblePrefab;
        public float spawnProbability;
    }

    [SerializeField] private CollectibleSpawnInfo[] collectibleTypes;
    [SerializeField] private float spawnInterval = 2f; // Tempo entre os spawns
    [SerializeField] private Vector2 spawnAreaSize; // Tamanho da área de spawn
    [SerializeField] private Vector2 spawnAreaCenter; // Centro da área de spawn

    private void Start()
    {
        StartCoroutine(SpawnCollectibles());
    }

    private IEnumerator SpawnCollectibles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnCollectible();
        }
    }

    private void SpawnCollectible()
    {
        float totalProbability = 0;

        foreach (var collectibleType in collectibleTypes)
        {
            totalProbability += collectibleType.spawnProbability;
        }

        float randomValue = Random.Range(0, totalProbability);

        float cumulativeProbability = 0;
        foreach (var collectibleType in collectibleTypes)
        {
            cumulativeProbability += collectibleType.spawnProbability;
            if (randomValue <= cumulativeProbability)
            {
                Vector2 spawnPosition = GetRandomSpawnPosition();
                Instantiate(collectibleType.collectiblePrefab, spawnPosition, Quaternion.identity);
                break;
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2f, spawnAreaCenter.x + spawnAreaSize.x / 2f);
        float randomY = Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2f, spawnAreaCenter.y + spawnAreaSize.y / 2f);
        return new Vector2(randomX, randomY);
    }

    // Desenha um gizmo para visualizar a área de spawn no Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 topLeft = new Vector3(spawnAreaCenter.x - spawnAreaSize.x / 2f, spawnAreaCenter.y + spawnAreaSize.y / 2f, 0);
        Vector3 topRight = new Vector3(spawnAreaCenter.x + spawnAreaSize.x / 2f, spawnAreaCenter.y + spawnAreaSize.y / 2f, 0);
        Vector3 bottomRight = new Vector3(spawnAreaCenter.x + spawnAreaSize.x / 2f, spawnAreaCenter.y - spawnAreaSize.y / 2f, 0);
        Vector3 bottomLeft = new Vector3(spawnAreaCenter.x - spawnAreaSize.x / 2f, spawnAreaCenter.y - spawnAreaSize.y / 2f, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
