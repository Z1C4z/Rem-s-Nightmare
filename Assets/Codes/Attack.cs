using UnityEngine;
using UnityEngine.InputSystem;

public class AttackManager : MonoBehaviour
{   

    [Header("Ataque")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackDuration = 0.2f;

    [Header("Hitbox")]
    [SerializeField] private GameObject BaseAttackHitbox;

    private bool isAttacking;
    private float attackTimer;
    private float cooldownTimer;

    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                isAttacking = false;
                cooldownTimer = attackCooldown;
                BaseAttackHitbox.SetActive(false);
            }
        }
    }

    public void OnAttack(InputValue value)
    {
        if (!isAttacking && cooldownTimer <= 0f)
        {
            isAttacking = true;
            attackTimer = attackDuration;
            BaseAttackHitbox.SetActive(true);

        }
    }
}