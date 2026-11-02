
using UnityEngine;

public static class States
{   
    public static bool isPaused = false;

    [SerializeField] public static int maxHealth = 100;
    [SerializeField] public static int currentHealth;

    public static Vector2 lastMoveDirection = Vector2.down;
    public static bool isDashing = false;
    public static bool canDash = true;

    public static Vector2 lookingAt = Vector2.zero;
    public static Vector2 PointerPosition = Vector2.zero;

    public static float dashInCooldown;
    public static float projectileCooldown;

    public static bool IsKnockedBack = false;

    public static float points = 0f;

    public static float bonusDamage = 0f;
}