using UnityEngine;
using UnityEngine.InputSystem;

public class DashManager : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody2D rb;

    private float dashTimer;
    private float cooldownTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnDash(InputValue value)
    {
        if (!States.isDashing && cooldownTimer <= 0f)
        {
            States.isDashing = true;
            dashTimer = dashDuration;

            rb.linearVelocity = States.lastMoveDirection * dashSpeed;
        }
    }

    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (States.isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                States.isDashing = false;
                cooldownTimer = dashCooldown;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    public float GetCooldownPercent()
    {
        return Mathf.Clamp01(cooldownTimer / dashCooldown);
    }
}