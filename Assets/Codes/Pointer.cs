using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    public Transform player;
    public float radius = 1.5f;
    public float angleOffset = -90f;

    public Vector2 PointerPosition { get; private set; }

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null || player == null) return;

        // Posição do mouse na tela
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 world = cam.ScreenToWorldPoint(
            new Vector3(screenPos.x, screenPos.y, -cam.transform.position.z));
        PointerPosition = world;

        // Direção do player até o mouse
        Vector2 direction = (PointerPosition - (Vector2)player.position).normalized;

        // Posição em órbita
        transform.position = (Vector2)player.position + direction * radius;

        // Rotação: a ponta aponta para o mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + angleOffset);
    }
}