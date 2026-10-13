using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Knockback : MonoBehaviour
{
    [SerializeField] private float force = 8f;
    [SerializeField] private float duration = 0.2f;

    private Rigidbody2D rb;
    private float endTime;
    private Coroutine knockbackCoroutine;

    public bool IsKnockedBack => Time.time < endTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Apply(Vector2 sourcePosition, bool isPlayer = false)
    {
        Vector2 direction = (Vector2)transform.position - sourcePosition;

        if (direction.sqrMagnitude < 0.001f)
            direction = Vector2.up;

        direction.Normalize();

        endTime = Time.time + duration;

        rb.linearVelocity = direction * force;

        if (isPlayer)
        {
            if (knockbackCoroutine != null)
                StopCoroutine(knockbackCoroutine);

            knockbackCoroutine = StartCoroutine(
                PlayerKnockbackRoutine()
            );
        }

        Debug.Log("Knockback aplicado: " + direction);
    }

    private IEnumerator PlayerKnockbackRoutine()
    {
        States.IsKnockedBack = true;

        yield return new WaitForSeconds(duration);

        States.IsKnockedBack = false;
        knockbackCoroutine = null;
    }
}