using System.Collections;
using UnityEngine;

public class RedBlink : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private int times = 3;

    private Coroutine Routine;
    private Color originalColor;

    private void Awake()
    {
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    public void Blink()
    {
        if (Routine != null) StopCoroutine(Routine);
        Routine = StartCoroutine(RoutineBlink());
    }

    private IEnumerator RoutineBlink()
    {
        for (int i = 0; i < times; i++)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(duration);
            sprite.color = originalColor;
            yield return new WaitForSeconds(duration);
        }
        sprite.color = originalColor;
        Routine = null;
    }
}