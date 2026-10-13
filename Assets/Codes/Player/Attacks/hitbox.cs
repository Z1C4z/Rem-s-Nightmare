using UnityEngine;
using UnityEngine.InputSystem;

public class hitbox : MonoBehaviour
{

    [SerializeField] private float attackDamage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Hitbox colidiu com inimigo: " + enemy.name);
                enemy.TakeDamage(attackDamage);
                enemy.gameObject.GetComponent<Knockback>().Apply(transform.position);
            }
        }
    }
}