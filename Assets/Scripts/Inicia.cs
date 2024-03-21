using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    // Método chamado quando o botão de iniciar o jogo é clicado
    public void StartGame()
    {
        Debug.Log("Iniciando o jogo..."); // Exibe uma mensagem no console
        // Carrega a cena do jogo
        SceneManager.LoadScene("Minigame");
    }
    
}
