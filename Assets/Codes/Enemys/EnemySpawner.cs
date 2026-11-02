using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Inimigo")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Tempo")]
    [SerializeField] private float spawnInterval = 2f;   // segundos entre spawns (valor inicial)
    [SerializeField] private float startDelay = 1f;      // espera antes do primeiro spawn

    [Header("Área")]
    [SerializeField] private Vector2 areaSize = new Vector2(10f, 6f);

    [Header("Limite (0 = sem limite)")]
    [SerializeField] private int maxAliveEnemies = 20;

    [Header("Dificuldade (a cada minuto)")]
    [SerializeField] private float secondsPerLevel = 60f;          // duração de cada nível
    [SerializeField] private float intervalReductionPerLevel = 0.15f; // reduz o intervalo (segundos)
    [SerializeField] private float minSpawnInterval = 0.3f;        // intervalo nunca fica menor que isso
    [SerializeField] private int extraMaxEnemiesPerLevel = 5;      // aumenta o limite de inimigos vivos
    [SerializeField] private int enemiesPerSpawnIncreaseEvery = 2; // a cada X níveis, +1 inimigo por spawn
    [SerializeField] private float healthMultiplierPerLevel = 0.2f; // +20% de vida por nível
    [SerializeField] private float speedMultiplierPerLevel = 0.05f; // +5% de velocidade por nível

    private float timer;
    private float elapsedTime;
    private int aliveCount;
    private int currentLevel; // 0 = primeiro minuto

    public int CurrentLevel => currentLevel;

    private void Start()
    {
        timer = startDelay;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateDifficulty();

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = GetCurrentInterval();

            int amount = GetEnemiesPerSpawn();
            for (int i = 0; i < amount; i++)
                TrySpawn();
        }
    }

    private void UpdateDifficulty()
    {
        int newLevel = Mathf.FloorToInt(elapsedTime / secondsPerLevel);

        if (newLevel != currentLevel)
        {
            currentLevel = newLevel;
            Debug.Log($"Dificuldade aumentou! Nível {currentLevel + 1}");
        }
    }

    private float GetCurrentInterval()
    {
        float interval = spawnInterval - currentLevel * intervalReductionPerLevel;
        return Mathf.Max(interval, minSpawnInterval);
    }

    private int GetMaxAlive()
    {
        if (maxAliveEnemies <= 0) return 0; // sem limite
        return maxAliveEnemies + currentLevel * extraMaxEnemiesPerLevel;
    }

    private int GetEnemiesPerSpawn()
    {
        if (enemiesPerSpawnIncreaseEvery <= 0) return 1;
        return 1 + currentLevel / enemiesPerSpawnIncreaseEvery;
    }

    private void TrySpawn()
    {
        if (enemyPrefab == null) return;

        int max = GetMaxAlive();
        if (max > 0 && aliveCount >= max) return;

        Vector2 center = transform.position;
        Vector2 randomPos = center + new Vector2(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
        );

        GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        aliveCount++;

        // Aplica os bônus de dificuldade no inimigo (se ele tiver o componente)
        Enemy stats = enemy.GetComponent<Enemy>();
        if (stats != null)
        {
            stats.ApplyDifficulty(
                1f + currentLevel * healthMultiplierPerLevel,
                1f + currentLevel * speedMultiplierPerLevel
            );
        }
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