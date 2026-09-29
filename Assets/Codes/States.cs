
using UnityEngine;
using UnityEngine.InputSystem;

public static class States
{
    public static bool isPaused = false;
    
    public static Vector2 lastMoveDirection = Vector2.down;

    public static bool isDashing = false;
    public static bool canDash = true;
}