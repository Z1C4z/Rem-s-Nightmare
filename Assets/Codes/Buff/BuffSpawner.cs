using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    [System.Serializable]
    public class BuffEntry
    {
        public GameObject prefab;
        [Min(0f)] public float weight = 1f;
    }

    
    [Header("Buffs")]
    [SerializeField] private List<BuffEntry> buffs = new List<BuffEntry>();

    [Header("Evolução dos buffs")]
    [SerializeField] private float levelUpInterval = 30f; 
    [SerializeField] private int healIncreasePerLevel = 5; 
    [SerializeField] private int damageIncreasePerLevel = 1;

private float elapsedTime;
private int currentLevel;

    [Header("Tempo")]
    [SerializeField] private float startDelay = 10f;
    [SerializeField] private float minInterval = 15f;  
    [SerializeField] private float maxInterval = 30f;   
    [SerializeField] private float buffLifetime = 20f;  

    [Header("Área")]
    [SerializeField] private Vector2 areaSize = new Vector2(10f, 6f);

    [Header("Limite")]
    [SerializeField] private int maxActiveBuffs = 3;

    [Header("Evitar spawn em cima do player (opcional)")]
    [SerializeField] private Transform player;
    [SerializeField] private float minDistanceFromPlayer = 2f;

    private float timer;
    private readonly List<GameObject> activeBuffs = new List<GameObject>();

    private void Start()
    {
        timer = startDelay;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        currentLevel = Mathf.FloorToInt(elapsedTime / levelUpInterval);

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = Random.Range(minInterval, maxInterval);
            TrySpawn();
        }
    }

    private void TrySpawn()
    {
        activeBuffs.RemoveAll(b => b == null);

        if (maxActiveBuffs > 0 && activeBuffs.Count >= maxActiveBuffs) return;

        GameObject prefab = PickRandomPrefab();
        if (prefab == null) return;

        Vector2 pos;
        if (!TryGetSpawnPosition(out pos)) return;

        GameObject buff = Instantiate(prefab, pos, Quaternion.identity);
       
        activeBuffs.Add(buff);
        Sfx.PlayAt("buffSpawnSFX", pos, 1f);

        if (buff.TryGetComponent(out BuffPickup pickup))
            pickup.ApplyLevelBonus(currentLevel, healIncreasePerLevel, damageIncreasePerLevel);

        if (buffLifetime > 0f)
            Destroy(buff, buffLifetime);
    }

    private GameObject PickRandomPrefab()
    {
        float totalWeight = 0f;
        foreach (BuffEntry entry in buffs)
            if (entry.prefab != null) totalWeight += entry.weight;

        if (totalWeight <= 0f) return null;

        float roll = Random.Range(0f, totalWeight);
        foreach (BuffEntry entry in buffs)
        {
            if (entry.prefab == null) continue;

            roll -= entry.weight;
            if (roll <= 0f) return entry.prefab;
        }

        return null;
    }

    private bool TryGetSpawnPosition(out Vector2 position)
    {
        Vector2 center = transform.position;

        for (int i = 0; i < 10; i++)
        {
            position = center + new Vector2(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
            );

            if (player == null || Vector2.Distance(position, player.position) >= minDistanceFromPlayer)
                return true;
        }

        position = Vector2.zero;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0.4f, 0.8f);
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}