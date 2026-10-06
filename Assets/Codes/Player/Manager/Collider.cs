using System.Diagnostics;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Health.TakeDamage(10);
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