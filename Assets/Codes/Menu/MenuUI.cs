using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void AbrirCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena); // carrega a nova e descarrega a antiga
        if (nomeDaCena == "StartManu")
        {
            States.isPaused = false; // Reseta o estado de pausa ao iniciar o jogo
        }
    }

    public void FecharJogo()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Quit não faz nada no Editor
#endif
    }
}