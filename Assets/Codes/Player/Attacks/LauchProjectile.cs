using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchProjectile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;

    [Header("Pointer")]
    [SerializeField] private Transform pointer;
    [SerializeField] private Vector2 tipLocalAxis = Vector2.up;

    [Header("Sprite do projétil")]
    [SerializeField] private float spriteAngleOffset = -90f;

    [Header("Cooldown")]
    [SerializeField] private float cooldown = 0.5f;
    private float nextLaunchTime;

    public void OnLaunchProjectile(InputValue value)
    {   
        if (States.isPaused) return;

        if (value.isPressed && Time.time >= nextLaunchTime)
        {
            Launch();
        }
    }

    private void Launch()
    {
        if (projectilePrefab == null)
        {
            return;
        }

        Vector2 origin;
        Vector2 direction;

        if (pointer != null)
        {
            // direcao da ponta do triangulo
            origin = pointer.position;
            direction = pointer.TransformDirection(tipLocalAxis.normalized);
        }
        else
        {
            origin = States.PointerPosition;
            direction = origin - (Vector2)transform.position;
        }

        if (direction.sqrMagnitude < 0.0001f)
        {
            return;
        }

        direction.Normalize();
        nextLaunchTime = Time.time + cooldown;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle + spriteAngleOffset);

        GameObject projectile = Instantiate(projectilePrefab, origin, rotation);

        if (!projectile.TryGetComponent(out Rigidbody2D rb))
        {
            Destroy(projectile);
            return;
        }

        rb.linearVelocity = direction * projectileSpeed;
        Destroy(projectile, projectileLifetime);
    }

    void Update()
    {
        States.projectileCooldown = CooldownRemaining / cooldown;
    }
    public float CooldownRemaining => Mathf.Max(0f, nextLaunchTime - Time.time);
}