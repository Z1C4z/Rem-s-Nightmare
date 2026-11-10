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
        States.currentHealth = States.maxHealth;
        States.points = 0f;
        States.bonusDamage = 0f;
        States.damageDealt = 0;
        States.enemiesKilled = 0;
        States.buffsPickedUp = 0;
        States.healsPickedUp = 0;
        States.damageTaken = 0;
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
        if (States.IsKnockedBack) return;
        
        if (!States.isDashing)
        {
            rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }
    }
}