using UnityEngine;

public class Flutuar : MonoBehaviour
{
    [Header("Vertical")]
    [SerializeField] private float amplitudeY = 0.25f;
    [SerializeField] private float velocidadeY = 2f;

    [Header("Horizontal")]
    [SerializeField] private float amplitudeX = 0.15f;
    [SerializeField] private float velocidadeX = 1.3f;

    private Vector2 deslocamentoAnterior;
    private float faseX;
    private float faseY;

    void Start()
    {
        faseX = Random.Range(0f, Mathf.PI * 2f);
        faseY = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        Vector2 deslocamentoAtual = new Vector2(
            Mathf.Sin(Time.time * velocidadeX + faseX) * amplitudeX,
            Mathf.Sin(Time.time * velocidadeY + faseY) * amplitudeY
        );

        Vector2 delta = deslocamentoAtual - deslocamentoAnterior;

        transform.position += (Vector3)delta;

        deslocamentoAnterior = deslocamentoAtual;
    }
}