using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Inimigo")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Tempo")]
    [SerializeField] private float spawnInterval = 2f;   // segundos entre spawns
    [SerializeField] private float startDelay = 1f;      // espera antes do primeiro spawn

    [Header("Área")]
    [SerializeField] private Vector2 areaSize = new Vector2(10f, 6f);

    [Header("Limite (0 = sem limite)")]
    [SerializeField] private int maxAliveEnemies = 20;

    private float timer;
    private int aliveCount;

    private void Start()
    {
        timer = startDelay;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = spawnInterval;
            TrySpawn();
        }
    }

    private void TrySpawn()
    {
        if (enemyPrefab == null) return;
        if (maxAliveEnemies > 0 && aliveCount >= maxAliveEnemies) return;

        Vector2 center = transform.position;
        Vector2 randomPos = center + new Vector2(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
        );

        GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        aliveCount++;
    }

    public void OnEnemyDestroyed()
    {
        aliveCount--;
    }

    // Desenha a área no editor (aparece na Scene View)
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.8f);
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}