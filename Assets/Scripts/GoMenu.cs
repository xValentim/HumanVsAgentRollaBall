using UnityEngine;
using UnityEngine.SceneManagement;

public class GoMenuButton : MonoBehaviour
{
    // Método chamado quando o botão de iniciar o jogo é clicado
    public void GoMenu()
    {
        Debug.Log("Iniciando o jogo..."); // Exibe uma mensagem no console
        // Carrega a cena do jogo
        SceneManager.LoadScene("Menu");
    }
    
}
