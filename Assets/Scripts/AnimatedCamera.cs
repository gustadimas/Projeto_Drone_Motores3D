using UnityEngine;

public class AnimatedCamera : MonoBehaviour
{
    [SerializeField] Transform target; // O objeto que representa o ponto de interesse da câmera
    [SerializeField] float speed = 10f; // Velocidade de rotação da câmera
    [SerializeField] float amplitude = 180.0f; // Amplitude do movimento do arco

    float angle = 0.0f; // Ângulo atual da animação
    int direction = 1; // Direção do movimento: 1 para sentido horário, -1 para sentido anti-horário.

    void LateUpdate()
    {
        // Atualiza o ângulo baseado no tempo e na velocidade
        angle += speed * Time.deltaTime * direction;

        // Verifica se uma volta completa foi completada
        if (angle >= 360.0f || angle <= -360.0f)
        {
            // Inverte a direção do movimento
            direction *= -1;
        }

        // Calcula a nova posição ao longo do arco
        float x = Mathf.Sin(angle * Mathf.Deg2Rad) * amplitude;
        float z = Mathf.Cos(angle * Mathf.Deg2Rad) * amplitude;
        Vector3 newPosition = target.position + new Vector3(x, 0, z);

        transform.position = newPosition;

        // Faz com que a câmera sempre aponte para o objeto central que está na cena.
        transform.LookAt(target);

    }



}
