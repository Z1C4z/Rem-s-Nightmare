using UnityEngine;
using TMPro;

public class ContadorTempo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoTempo;

    private float tempoDecorrido;
    private bool contando = true;

    void Update()
    {
        if (!contando) return;

        tempoDecorrido += Time.deltaTime;

        int minutos = Mathf.FloorToInt(tempoDecorrido / 60f);
        int segundos = Mathf.FloorToInt(tempoDecorrido % 60f);

        textoTempo.text = $"{minutos:00}:{segundos:00}";
    }

    public void Pausar() => contando = false;
    public void Continuar() => contando = true;

    public void Reiniciar()
    {
        tempoDecorrido = 0f;
        contando = true;
    }

    public int MinutosPassados() => Mathf.FloorToInt(tempoDecorrido / 60f);
}