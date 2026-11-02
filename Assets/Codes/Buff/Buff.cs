using UnityEngine;

public class BuffPickup : MonoBehaviour
{
    [Header("Efeitos (deixe 0 para desativar)")]
    [SerializeField] private int healAmount = 0;
    [SerializeField] private int damageBonus = 0;

    [Header("Config")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool onlyPickupIfHurt = false; // se true, cura só é coletada com vida incompleta

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

       States.currentHealth += healAmount;
       States.bonusDamage += damageBonus;

        Destroy(gameObject);
    }

    public void ApplyLevelBonus(int level, int healPerLevel, int damagePerLevel)
    {
        // só aumenta o efeito que o buff já tem (cura ou dano)
        if (healAmount > 0) healAmount += level * healPerLevel;
        if (damageBonus > 0) damageBonus += level * damagePerLevel;
    }
}