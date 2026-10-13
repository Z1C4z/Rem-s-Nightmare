using UnityEngine;
using UnityEngine.InputSystem;

public class SpinAttack : MonoBehaviour
{
    [Header("Spin Attack Settings")]
    [SerializeField] private float damage = 20f;
    [SerializeField] private float duration = 1f;

    [SerializeField] private Collider2D attackCollider;

    void Awake()
    {
        attackCollider.enabled = false;
    }

    public void OnSpinAttack(InputValue value)
    {
        if (States.isPaused) return;

        if (value.isPressed)
        {
            StartSpinAttack();
        }
    }

    public void StartSpinAttack()
    {
        attackCollider.enabled = true;
        Invoke("EndSpinAttack", duration);
    }

    private void EndSpinAttack()
    {
        attackCollider.enabled = false;
    }
}