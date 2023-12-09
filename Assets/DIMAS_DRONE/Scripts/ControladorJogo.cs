using UnityEngine;
using Cinemachine;

public class ControladorJogo : MonoBehaviour
{
    [SerializeField] ControladorDrone[] drones;
    [SerializeField] CinemachineVirtualCamera[] camerasDrone, camerasAmbiente;

    [Header("Posições Câmeras Drone Rosa")]
    [SerializeField] Transform primeiraPessoaFrontalDroneRosa;
    [SerializeField] Transform terceiraPessoaFrontalDroneRosa;
    [SerializeField] Transform terceiraPessoaTraseiraDroneRosa;

    [Header("Posições Câmeras Drone Azul")]
    [SerializeField] Transform primeiraPessoaFrontalDroneAzul;
    [SerializeField] Transform terceiraPessoaFrontalDroneAzul;
    [SerializeField] Transform terceiraPessoaTraseiraDroneAzul;

    [Header("Posições Câmeras Drone Verde")]
    [SerializeField] Transform primeiraPessoaFrontalDroneVerde;
    [SerializeField] Transform terceiraPessoaFrontalDroneVerde;
    [SerializeField] Transform terceiraPessoaTraseiraDroneVerde;

    [Header("Posições Câmeras Drone Amarelo")]
    [SerializeField] Transform primeiraPessoaFrontalDroneAmarelo;
    [SerializeField] Transform terceiraPessoaFrontalDroneAmarelo;
    [SerializeField] Transform terceiraPessoaTraseiraDroneAmarelo;

    [Header("Posições Câmeras Drone Vermelho")]
    [SerializeField] Transform primeiraPessoaFrontalDroneVermelho;
    [SerializeField] Transform terceiraPessoaFrontalDroneVermelho;
    [SerializeField] Transform terceiraPessoaTraseiraDroneVermelho;

    int indiceDroneAtual = 0;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AtivarCameraPrimeiraPessoaFrontal();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
          AtivarCameraTerceiraPessoaFrontal();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AtivarCameraTerceiraPessoaTraseira();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AtivarCameraAmbienteFixa();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AtivarCameraAmbienteAnimada();
        }
    }

    void AtivarCameraAmbienteFixa()
    {
        DesfocarTodasCameras();
        camerasAmbiente[0].Priority = 100;
    }

    void AtivarCameraAmbienteAnimada()
    {
        DesfocarTodasCameras();
        camerasAmbiente[1].Priority = 100;
    }

    void ProximoDrone()
    {
        indiceDroneAtual = (indiceDroneAtual + 1) % drones.Length;
        ConfigurarAlvoParaDrone();
    }

    void DroneAnterior()
    {
        indiceDroneAtual = (indiceDroneAtual - 1 + drones.Length) % drones.Length;
        ConfigurarAlvoParaDrone();
    }

    void AtivarCameraPrimeiraPessoaFrontal()
    {
        DesfocarTodasCameras();
        camerasDrone[0].Priority = 100;
    }

    void AtivarCameraTerceiraPessoaFrontal()
    {
        DesfocarTodasCameras();
        camerasDrone[1].Priority = 100;
    }

    void AtivarCameraTerceiraPessoaTraseira()
    {
        DesfocarTodasCameras();
        camerasDrone[2].Priority = 100;
    }

    void DesfocarTodasCameras()
    {
        foreach (CinemachineVirtualCamera camera in camerasDrone)
        {
            camera.Priority = 0;
        }

        foreach (CinemachineVirtualCamera camera in camerasAmbiente)
        {
            camera.Priority = 0;
        }
    }

    void ConfigurarAlvoParaDrone()
    {
        switch (indiceDroneAtual)
        {
            case 0: // Drone Rosa
                camerasDrone[0].LookAt = primeiraPessoaFrontalDroneRosa;
                camerasDrone[0].Follow = primeiraPessoaFrontalDroneRosa;
                camerasDrone[1].LookAt = terceiraPessoaFrontalDroneRosa;
                camerasDrone[1].Follow = terceiraPessoaFrontalDroneRosa;
                camerasDrone[2].LookAt = terceiraPessoaTraseiraDroneRosa;
                camerasDrone[2].Follow = terceiraPessoaTraseiraDroneRosa;
                break;
            case 1: // Drone Azul
                camerasDrone[0].LookAt = primeiraPessoaFrontalDroneAzul;
                camerasDrone[0].Follow = primeiraPessoaFrontalDroneAzul;
                camerasDrone[1].LookAt = terceiraPessoaFrontalDroneAzul;
                camerasDrone[1].Follow = terceiraPessoaFrontalDroneAzul;
                camerasDrone[2].LookAt = terceiraPessoaTraseiraDroneAzul;
                camerasDrone[2].Follow = terceiraPessoaTraseiraDroneAzul;
                break;
            case 2: // Drone Verde
                camerasDrone[0].LookAt = primeiraPessoaFrontalDroneVerde;
                camerasDrone[0].Follow = primeiraPessoaFrontalDroneVerde;
                camerasDrone[1].LookAt = terceiraPessoaFrontalDroneVerde;
                camerasDrone[1].Follow = terceiraPessoaFrontalDroneVerde;
                camerasDrone[2].LookAt = terceiraPessoaTraseiraDroneVerde;
                camerasDrone[2].Follow = terceiraPessoaTraseiraDroneVerde;
                break;
            case 3: // Drone Amarelo
                camerasDrone[0].LookAt = primeiraPessoaFrontalDroneAmarelo;
                camerasDrone[0].Follow = primeiraPessoaFrontalDroneAmarelo;
                camerasDrone[1].LookAt = terceiraPessoaFrontalDroneAmarelo;
                camerasDrone[1].Follow = terceiraPessoaFrontalDroneAmarelo;
                camerasDrone[2].LookAt = terceiraPessoaTraseiraDroneAmarelo;
                camerasDrone[2].Follow = terceiraPessoaTraseiraDroneAmarelo;
                break;
            case 4: // Drone Vermelho
                camerasDrone[0].LookAt = primeiraPessoaFrontalDroneVermelho;
                camerasDrone[0].Follow = primeiraPessoaFrontalDroneVermelho;
                camerasDrone[1].LookAt = terceiraPessoaFrontalDroneVermelho;
                camerasDrone[1].Follow = terceiraPessoaFrontalDroneVermelho;
                camerasDrone[2].LookAt = terceiraPessoaTraseiraDroneVermelho;
                camerasDrone[2].Follow = terceiraPessoaTraseiraDroneVermelho;
                break;
            default:
                break;
        }
    }
 
}
