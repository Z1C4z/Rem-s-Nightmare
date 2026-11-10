using UnityEngine;

public class GirarSemPararFisica : MonoBehaviour
{
    [SerializeField] private float velocidade = 90f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation + velocidade * Time.fixedDeltaTime);
    }
}