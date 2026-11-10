using UnityEngine;
using UnityEngine.SceneManagement;

public class Health: MonoBehaviour
{
    void Awake()
    {
        States.currentHealth = States.maxHealth;
    }

    public static void TakeDamage(int damage)
    {
        States.currentHealth -= damage;
        States.damageTaken += damage;
    
        Sfx.Play("playerDmgSFX", volume: 0.4f);

        if (States.currentHealth <= 0)
        {
            Die();
        }
    }

    private static void Die()
    {
        Debug.Log("Player has died.");
        Sfx.Play("deathSFX", 1f);
        SceneManager.LoadScene("DeathScreen");
    }
}