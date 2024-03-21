using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float timeRemaining = 60f; // Tempo total em segundos
    public Text timerText; // Referência ao objeto de texto que mostrará o timer
    private bool timerIsRunning = false; // Verifica se o temporizador está em execução

    void Start()
    {
        // Configura o temporizador para estar em execução
        timerIsRunning = true;
    }

    void Update()
    {
        // Verifica se o temporizador está em execução
        if (timerIsRunning)
        {
            // Verifica se o tempo restante é maior que 0
            if (timeRemaining > 0)
            {
                // Decrementa o tempo restante
                timeRemaining -= Time.deltaTime;
                // Atualiza o texto do timer
                DisplayTime(timeRemaining);
            }
            else
            {
                // Caso o tempo acabe, define o texto do timer como 0 e para o temporizador
                timeRemaining = 0;
                DisplayTime(timeRemaining);
                timerIsRunning = false;
                // Adicione aqui qualquer ação que deseje executar quando o tempo acabe
                Debug.Log("Tempo acabou!");
            }
        }
    }

    // Método para exibir o tempo formatado no objeto de texto
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); // Calcula os minutos
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // Calcula os segundos

        // Atualiza o texto do objeto de texto para exibir o tempo restante
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetActiveFalse()
    {
        
        timerText.gameObject.SetActive(false);
        timerIsRunning = false;

    }
}

