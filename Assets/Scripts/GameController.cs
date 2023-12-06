using UnityEngine;
using TMPro;
using Cinemachine;

public class GameController : MonoBehaviour
{
    [SerializeField] CarController[] cars;
    [SerializeField] CinemachineVirtualCamera[] carCameras, ambientCameras;
    [SerializeField] TextMeshProUGUI pilotCarName;

    [Header("Posicoes Cameras Carro Dourado")]
    [SerializeField] Transform goldenCarFirstPersonFrontalCamera;
    [SerializeField] Transform goldenCarFirstPersonBackCamera;
    [SerializeField] Transform goldenCarThirdPersonFrontalCamera;
    [SerializeField] Transform goldenCarThirdPersonBackCamera;

    [Header("Posicoes Cameras Carro Amarelo")]
    [SerializeField] Transform yellowCarFirstPersonFrontalCamera;
    [SerializeField] Transform yellowCarFirstPersonBackCamera;
    [SerializeField] Transform yellowCarThirdPersonFrontalCamera;
    [SerializeField] Transform yellowCarThirdPersonBackCamera;

    [Header("Posicoes Cameras Carro Vermelho")]
    [SerializeField] Transform redCarFirstPersonFrontalCamera;
    [SerializeField] Transform redCarFirstPersonBackCamera;
    [SerializeField] Transform redCarThirdPersonFrontalCamera;
    [SerializeField] Transform redCarThirdPersonBackCamera;

    int currentCarIndex = 0;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Start(){
        SetPilotNameText();
    }

    public void ActivateFixedAmbientCamera(){
        UnfocusAllCameras();
        ambientCameras[0].Priority = 100;
    }

    public void ActivateAnimatedAmbientCamera(){
        UnfocusAllCameras();
        ambientCameras[1].Priority = 100;
    }

    public void ActivateCarAmbientCamera(){
        UnfocusAllCameras();
        ambientCameras[2].Priority = 100;
    }

    public void NextCar(){
        currentCarIndex = (currentCarIndex + 1) % cars.Length;
        SetPilotNameText();
        SetLookAndFollowTargetToCar();
    }

    public void PreviousCar(){
        currentCarIndex = (currentCarIndex - 1) % cars.Length;
        if (currentCarIndex < 0)
        {
            currentCarIndex = cars.Length - 1;
        }
        SetPilotNameText();
        SetLookAndFollowTargetToCar();
    }

    public void ActivateFirstPersonFrontalCamera(){
        UnfocusAllCameras();
        carCameras[0].Priority = 100;
    }

    public void ActivateFirstPersonBackCamera(){
        UnfocusAllCameras();
        carCameras[1].Priority = 100;
    }

    public void ActivateThirdPersonFrontalCamera(){
        UnfocusAllCameras();
        carCameras[2].Priority = 100;
    }

    public void ActivateThirdPersonBackCamera(){
        UnfocusAllCameras();
        carCameras[3].Priority = 100;
    }

    void UnfocusAllCameras(){
        foreach(CinemachineVirtualCamera camera in carCameras){
            camera.Priority = 0;
        }

        foreach(CinemachineVirtualCamera camera in ambientCameras){
            camera.Priority = 0;
        }
    }

    void SetPilotNameText(){
        pilotCarName.text = cars[currentCarIndex].GetPilotName();
    }

    void SetLookAndFollowTargetToCar(){
        if(currentCarIndex == 0){
            carCameras[0].LookAt = goldenCarFirstPersonFrontalCamera;
            carCameras[0].Follow = goldenCarFirstPersonFrontalCamera;
            carCameras[1].LookAt = goldenCarFirstPersonBackCamera;
            carCameras[1].Follow = goldenCarFirstPersonBackCamera;
            carCameras[2].LookAt = goldenCarThirdPersonFrontalCamera;
            carCameras[2].Follow = goldenCarThirdPersonFrontalCamera;
            carCameras[3].LookAt = goldenCarThirdPersonBackCamera;
            carCameras[3].Follow = goldenCarThirdPersonBackCamera;
        }
        else if(currentCarIndex == 1){
            carCameras[0].LookAt = yellowCarFirstPersonFrontalCamera;
            carCameras[0].Follow = yellowCarFirstPersonFrontalCamera;
            carCameras[1].LookAt = yellowCarFirstPersonBackCamera;
            carCameras[1].Follow = yellowCarFirstPersonBackCamera;
            carCameras[2].LookAt = yellowCarThirdPersonFrontalCamera;
            carCameras[2].Follow = yellowCarThirdPersonFrontalCamera;
            carCameras[3].LookAt = yellowCarThirdPersonBackCamera;
            carCameras[3].Follow = yellowCarThirdPersonBackCamera;
        }
        else if(currentCarIndex == 2){
            carCameras[0].LookAt = redCarFirstPersonFrontalCamera;
            carCameras[0].Follow = redCarFirstPersonFrontalCamera;
            carCameras[1].LookAt = redCarFirstPersonBackCamera;
            carCameras[1].Follow = redCarFirstPersonBackCamera;
            carCameras[2].LookAt = redCarThirdPersonFrontalCamera;
            carCameras[2].Follow = redCarThirdPersonFrontalCamera;
            carCameras[3].LookAt = redCarThirdPersonBackCamera;
            carCameras[3].Follow = redCarThirdPersonBackCamera;
        }
    }

}
