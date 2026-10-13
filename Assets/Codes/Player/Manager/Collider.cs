using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] private Knockback knockback;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Health.TakeDamage(10);
                Debug.Log("Colidiu com inimigo!");
                knockback.Apply(collision.transform.position, true);
                break;
            case "Obstacle":
                //Debug.Log("Colidiu com o obstáculo!");
                break;
            default:
                //Debug.Log("Colidiu com um objeto desconhecido!");
                break;
        }
    }
}