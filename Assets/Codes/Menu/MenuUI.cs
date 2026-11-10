using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI damageDealtText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private TextMeshProUGUI buffsPickedUpText;
    [SerializeField] private TextMeshProUGUI healsPickedUpText;
    [SerializeField] private TextMeshProUGUI damageTakenText;


    void Start()
    {
        pointsText.text = $"Pontos: {States.points}";
        damageDealtText.text = $"Dano causado: {States.damageDealt}";
        enemiesKilledText.text = $"Inimigos Eliminados: {States.enemiesKilled}";
        buffsPickedUpText.text = $"Buffs Coletados: {States.buffsPickedUp}";
        healsPickedUpText.text = $"Cura Coletada: {States.healsPickedUp}";
        damageTakenText.text = $"Dano Recebido: {States.damageTaken}";
    }

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