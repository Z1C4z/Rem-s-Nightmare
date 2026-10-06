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
        //States.currentHealth = Mathf.Clamp(States.currentHealth, 0, States.maxHealth);

        if (States.currentHealth <= 0)
        {
            Die();
        }
    }

    private static void Die()
    {
        Debug.Log("Player has died.");
        SceneManager.LoadScene("DeathScreen");
    }
    

    //public int GetCurrentHealth()
    //{
    //    return States.currentHealth;
    //}

    //public int GetMaxHealth()
    //{
     //   return States.maxHealth;
    //}
}