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

        // Calcula a rotação para olhar na direção do próximo ponto
        Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);

        // Rotaciona suavemente em direção ao próximo ponto
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

            // Aleatoriamente executa uma rotação de 360 graus horizontal
            if (Random.value < 0.2f) // O valor aqui pode ser alterado para mudar a frequência da manobra
            {
                Quaternion rotacaoAleatoria = Quaternion.Euler(1, 1, Random.Range(0, 360));
                transform.rotation *= rotacaoAleatoria;
            }

            // Altera a velocidade do drone a cada mudança de ponto
            velocidadeAtual = Random.Range(velocidadeMinima, velocidadeMaxima);
        }
    }
}
