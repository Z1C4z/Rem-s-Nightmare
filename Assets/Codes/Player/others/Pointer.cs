using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    public Transform player;
    public float radius = 1.5f;
    public float angleOffset = -90f;
    public float stickDeadzone = 0.2f;

    public Vector2 PointerPosition { get; private set; }

    Camera cam;
    Vector2 lastDirection = Vector2.right;
    Vector2 lastMousePos;
    bool usingGamepad;

    void Start()
    {
        cam = Camera.main;
        if (Mouse.current != null)
            lastMousePos = Mouse.current.position.ReadValue();
    }

    void Update()
    {
        if (States.isPaused) return;
        if (player == null) return;

        var mouse = Mouse.current;
        var gamepad = Gamepad.current;

        Vector2 stick = gamepad != null ? gamepad.rightStick.ReadValue() : Vector2.zero;
        bool stickActive = stick.magnitude > stickDeadzone;

        bool mouseMoved = false;
        if (mouse != null)
        {
            Vector2 mousePos = mouse.position.ReadValue();
            mouseMoved = (mousePos - lastMousePos).sqrMagnitude > 0.01f;
            lastMousePos = mousePos;
        }

        if (stickActive) usingGamepad = true;
        else if (mouseMoved) usingGamepad = false;

        Vector2 direction;

        if (usingGamepad)
        {
            if (stickActive) lastDirection = stick.normalized;
            direction = lastDirection;
            PointerPosition = (Vector2)player.position + direction * radius;
        }
        else
        {
            if (mouse == null || cam == null) return;

            Vector2 screenPos = mouse.position.ReadValue();
            Vector3 world = cam.ScreenToWorldPoint(
                new Vector3(screenPos.x, screenPos.y, -cam.transform.position.z));
            PointerPosition = world;

            Vector2 diff = PointerPosition - (Vector2)player.position;
            if (diff.sqrMagnitude > 0.0001f) lastDirection = diff.normalized;
            direction = lastDirection;
        }

        States.lookingAt = direction;

        transform.position = (Vector2)player.position + direction * radius;
        States.PointerPosition = transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + angleOffset);
    }
}