using UnityEngine;

public class TrocarSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteBaixo;
    [SerializeField] private Sprite spriteCima;
    [SerializeField] private Sprite spriteLado;

    private Vector3 posicaoAnterior;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        posicaoAnterior = transform.position;
    }

    void LateUpdate()
    {
        Vector2 movimento = transform.position - posicaoAnterior;
        posicaoAnterior = transform.position;

        // parado: mantém o último sprite
        if (movimento.sqrMagnitude < 0.0000001f) return;

        if (Mathf.Abs(movimento.x) > Mathf.Abs(movimento.y))
        {
            spriteRenderer.sprite = spriteLado;
            spriteRenderer.flipX = movimento.x > 0;
        }
        else
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = movimento.y > 0 ? spriteCima : spriteBaixo;
        }
    }
}