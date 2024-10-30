using Cinemachine;
using StarterAssets;
using UnityEngine;

public class CamController : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    Cinemachine3rdPersonFollow fristCam;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        fristCam = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }
    
    public void ViewConversion()
    {
        switch(CharacterManager.Instance.Player.controller.conversion)
        {
            case true:
                FirstPersonView();
                break;
            case false:
                ThirdPersonView();
                break;
        }
    }

    public void FirstPersonView()
    {
        fristCam.ShoulderOffset.Set(0f, 0.5f, 0);
        fristCam.CameraDistance = -0.5f;
    }

    public void ThirdPersonView()
    {
        fristCam.ShoulderOffset.Set(0.1f, 0.25f, 0.3f);
        fristCam.CameraDistance = 4;
    }
}