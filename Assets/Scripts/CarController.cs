using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(NavMeshAgent))]
public class CarController : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] Transform frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel;
    [SerializeField] float carSpeed = 5f;
    [SerializeField] float carRotationSpeed = 5f;
    [SerializeField] AudioClip carSound;
    [SerializeField] TextMeshProUGUI carSpeedText;
    [SerializeField] TextMeshProUGUI carPilotNameText;
    [SerializeField] string carPilotName = "Roda Presa";
    [SerializeField] GameObject smokeParticleBackLeft, smokeParticleBackRight;

    NavMeshAgent carAgent;
    AudioSource audioSource;

    int waypointIndex = 0;
    bool onCarFocus = false;
    float rotationThreshold = 15f; // Limite de rotação para detectar uma curva fechada

    void Start()
    {        
        SetPilotNameText();
        SetupAgent();
        SetupAudio();
        StartCoroutine(Accelerate());
    }

    void SetupAudio(){
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = carSound;
        audioSource.Play();
    }

    void SetupAgent(){
        carAgent = GetComponent<NavMeshAgent>();
        carAgent.speed = carSpeed;
        carAgent.acceleration = carSpeed * 2;
        carAgent.angularSpeed = carRotationSpeed;
        carAgent.autoBraking = false;
        carAgent.updateRotation = false;
    }

    void FixedUpdate()
    {
        MoveCar();
        UpdateCarSpeedText();
    }

    void MoveCar(){
        float distance = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if(distance < 1.5f){
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
        carAgent.SetDestination(waypoints[waypointIndex].position);
        RotateWheels();
        RotateCar();        
    }

    void RotateWheels(){
        frontLeftWheel.Rotate(carRotationSpeed * Time.deltaTime * 10, 0f, 0f);
        frontRightWheel.Rotate(carRotationSpeed * Time.deltaTime * 10, 0f, 0f);
        backLeftWheel.Rotate(carRotationSpeed * Time.deltaTime * 10, 0f, 0f);
        backRightWheel.Rotate(carRotationSpeed * Time.deltaTime * 10, 0f, 0f);
    }

    void RotateCar(){
        Vector3 direction = carAgent.steeringTarget - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * carRotationSpeed);
        
        // Verifica se a diferença de rotação é maior que o limite para detectar uma curva fechada
        float angleDifference = Quaternion.Angle(transform.rotation, rotation);
        if (angleDifference > rotationThreshold)
        {
            ActivateSmokeParticle();
        } else
        {
            DeactivateSmokeParticle();
        }

    }

    void ActivateSmokeParticle()
    {
        smokeParticleBackLeft.SetActive(true);
        smokeParticleBackRight.SetActive(true);
    }

    void DeactivateSmokeParticle()
    {
        smokeParticleBackLeft.SetActive(false);
        smokeParticleBackRight.SetActive(false);
    }

    void UpdateCarSpeedText(){
        carSpeedText.text = ConvertAgentSpeedToKmH().ToString("0.0") + " km/h";
    }

    void SetPilotNameText(){
        carPilotNameText.text = carPilotName;
    }

    float ConvertAgentSpeedToKmH(){
        return carAgent.velocity.magnitude * 3.6f;
    }

    public string GetPilotName(){
        return carPilotName;
    }


    IEnumerator Accelerate()
    {
        float targetSpeed = carSpeed;
        float currentSpeed = carSpeed;

        while (true)
        {
            // Acelera gradualmente até a velocidade alvo
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime);
            carAgent.speed = currentSpeed;

            // Atualiza o texto de velocidade do carro
            UpdateCarSpeedText();

            // Espera um curto período de tempo antes de continuar
            yield return new WaitForSeconds(0.1f);

            // Verifica se o carro está próximo de uma curva
             Quaternion rot = Quaternion.LookRotation(carAgent.steeringTarget - transform.position);
            float angleDifference = Quaternion.Angle(transform.rotation, rot);
            if (angleDifference > rotationThreshold)
            {
                // Se estiver próximo de uma curva, diminui a velocidade
                targetSpeed = carSpeed * 0.5f;
            }
            else
            {
                // Caso contrário, aumenta a velocidade para a velocidade máxima
                targetSpeed = carSpeed;
            }
        }
    }    

}
