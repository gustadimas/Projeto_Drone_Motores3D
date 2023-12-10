using UnityEngine;

public class GiraCamera : MonoBehaviour
{
    [SerializeField] Transform target; 
    [SerializeField] float velocidade = 10f;
    [SerializeField] float amplitude = 180.0f; 

    float angulo = 0.0f; 
    int direcao = 1;

    void LateUpdate()
    {
        angulo += velocidade * Time.deltaTime * direcao;

        if (angulo >= 360.0f || angulo <= -360.0f)
        {
            direcao *= -1;
        }

        float x = Mathf.Sin(angulo * Mathf.Deg2Rad) * amplitude;
        float z = Mathf.Cos(angulo * Mathf.Deg2Rad) * amplitude;
        Vector3 novaPosicao = target.position + new Vector3(x, 0, z);

        transform.position = novaPosicao;

        transform.LookAt(target);
    }
}
