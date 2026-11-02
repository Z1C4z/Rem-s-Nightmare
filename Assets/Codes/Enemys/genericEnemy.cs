using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float maxHealth = 30f;
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float detectionRange = 6f;
    [SerializeField] protected float stopDistance = 0.5f;
    [SerializeField] protected float points = 5f;

    [Header("Alvo")]
    [SerializeField] protected Transform target;
    protected Rigidbody2D rb;
    protected Knockback knockback;
    protected float currentHealth;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        currentHealth = maxHealth;
    }

    protected virtual void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (knockback.IsKnockedBack) return;
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= detectionRange && distance > stopDistance)
            Move();
        else
            rb.linearVelocity = Vector2.zero;
    }

    protected virtual void Move()
    {
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f) Die();
    }

    protected virtual void Die()
    {
        States.points += points;
        Destroy(gameObject);
    }

    public void ApplyDifficulty(float healthMultiplier, float speedMultiplier)
    {
        maxHealth *= healthMultiplier;
        moveSpeed *= speedMultiplier;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}