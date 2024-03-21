using UnityEngine;

public class Cronometro : MonoBehaviour
{
    private float tempoDecorrido = 0f;
    private bool cronometroAtivo = false;

    void Update()
    {
        if (cronometroAtivo)
        {
            tempoDecorrido += Time.deltaTime;
        }
    }

    public void IniciarCronometro()
    {
        cronometroAtivo = true;
    }

    public void PararCronometro()
    {
        cronometroAtivo = false;
    }

    public void ReiniciarCronometro()
    {
        tempoDecorrido = 0f;
    }

    public float TempoDecorrido()
    {
        return tempoDecorrido;
    }
}
