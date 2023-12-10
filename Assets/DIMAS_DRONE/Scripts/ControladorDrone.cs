using UnityEngine;

public class ControladorDrone : MonoBehaviour
{
    public Transform[] pontosTrajetoria;
    public float velocidadeMinima = 60.0f;
    public float velocidadeMaxima = 90.0f;
    public float velocidadeRotacao = 2.0f;
    public float suavidade = 5.0f; // Suavidade da transição entre pontos

    int pontoInicial = 0;
    int pontoFinal = 1;
    float progresso = 0.0f;
    float velocidadeAtual;

    void Start()
    {
        velocidadeAtual = Random.Range(velocidadeMinima, velocidadeMaxima);
    }

    void Update()
    {
        if (pontosTrajetoria.Length == 0)
            return;

        Vector3 direcao = (pontosTrajetoria[pontoFinal].position - pontosTrajetoria[pontoInicial].position).normalized;
        float distancia = Vector3.Distance(pontosTrajetoria[pontoInicial].position, pontosTrajetoria[pontoFinal].position);

        transform.position = Vector3.Lerp(pontosTrajetoria[pontoInicial].position, pontosTrajetoria[pontoFinal].position, progresso);

        Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, velocidadeRotacao * Time.deltaTime);

        progresso += Time.deltaTime * velocidadeAtual / distancia;

        if (progresso >= 1.0f)
        {
            progresso = 0.0f;
            pontoInicial++;
            pontoFinal++;

            if (pontoFinal >= pontosTrajetoria.Length)
                pontoFinal = 0;

            if (pontoInicial >= pontosTrajetoria.Length)
                pontoInicial = 0;

            if (Random.value < 0.2f) // pode ser alterado para mudar a frequência da manobra
            {
                Quaternion rotacaoAleatoria = Quaternion.Euler(1, 1, Random.Range(0, 360));
                transform.rotation *= rotacaoAleatoria;
            }

            velocidadeAtual = Random.Range(velocidadeMinima, velocidadeMaxima);
        }
    }
}
