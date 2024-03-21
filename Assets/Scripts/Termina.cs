using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    // Método chamado quando o botão de sair do jogo é clicado
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo..."); // Exibe uma mensagem no console
        // Fecha o aplicativo (somente em build executável, não no editor Unity)
        Application.Quit();
    }
}
