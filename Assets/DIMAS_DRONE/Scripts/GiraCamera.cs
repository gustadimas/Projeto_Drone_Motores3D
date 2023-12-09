using UnityEngine;

public class GiraCamera : MonoBehaviour
{
    [SerializeField] Transform target; // O objeto que representa o ponto de interesse da câmera
    [SerializeField] float velocidade = 10f; // Velocidade de rotação da câmera
    [SerializeField] float amplitude = 180.0f; // Amplitude do movimento do arco

    float angulo = 0.0f; // Ângulo atual da animação
    int direcao = 1; // Direção do movimento: 1 para sentido horário, -1 para sentido anti-horário.

    void LateUpdate()
    {
        // Atualiza o ângulo baseado no tempo e na velocidade
        angulo += velocidade * Time.deltaTime * direcao;

        // Verifica se uma volta completa foi completada
        if (angulo >= 360.0f || angulo <= -360.0f)
        {
            // Inverte a direção do movimento
            direcao *= -1;
        }

        // Calcula a nova posição ao longo do arco
        float x = Mathf.Sin(angulo * Mathf.Deg2Rad) * amplitude;
        float z = Mathf.Cos(angulo * Mathf.Deg2Rad) * amplitude;
        Vector3 novaPosicao = target.position + new Vector3(x, 0, z);

        transform.position = novaPosicao;

        // Faz com que a câmera sempre aponte para o objeto central que está na cena.
        transform.LookAt(target);
    }
}
