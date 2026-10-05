using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPausa; // painel do menu (começa desativado)
    bool pausado = false; // estado do jogo

    void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            if (pausado) Retomar();
            else Pausar();
        }
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f; // congela o jogo
        States.isPaused = true;
        pausado = true;
    }

    public void Retomar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        States.isPaused = false;
        pausado = false;
    }
}