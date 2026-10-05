using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    bool invulneravel;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (invulneravel || !collision.gameObject.CompareTag("Enemy")) return;

        // playerHealth = GetComponent<Health>();
        //playerHealth.TakeDamage(10);

        //RedBlink redBlink = GetComponent<RedBlink>();
        //redBlink.Blink();

        // empurra o player para longe do inimigo
        Vector2 direcao = (transform.position - collision.transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(direcao * 8f, ForceMode2D.Impulse);

        invulneravel = true;
        Invoke(nameof(FimInvulnerabilidade), 1f);
    }

    void FimInvulnerabilidade() => invulneravel = false;
}