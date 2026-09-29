using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMovement : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput != Vector2.zero)
        {
            States.lastMoveDirection = moveInput.normalized;
        }
    }
    

    void FixedUpdate()
    {
        if (!States.isDashing)
        {
            rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }
    }
}