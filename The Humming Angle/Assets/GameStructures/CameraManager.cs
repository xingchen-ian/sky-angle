using UnityEngine;
using Cinemachine;
using StarterAssets;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; } // 单例

    [Header("Cinemachine Cameras")]
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;

    private StarterAssetsInputs input;
    private bool isThirdPerson = false;
    
    void Awake()
    {
        // 确保单例唯一
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        input = FindObjectOfType<StarterAssetsInputs>();

        // 确保游戏开始时是第一人称
        SetCameraView(false);
    }

    public void SetCameraView(bool enableThirdPerson)
    {
        if (isThirdPerson == enableThirdPerson) return;
        isThirdPerson = enableThirdPerson;

        if (isThirdPerson)
        {
            thirdPersonCamera.Priority = 10;
            firstPersonCamera.Priority = 5;
        }
        else
        {
            thirdPersonCamera.Priority = 5;
            firstPersonCamera.Priority = 10;
        }
    }

    public void OnDroneApproaching(bool droneIsNear)
    {
        if (droneIsNear && !isThirdPerson)
        {
            SetCameraView(true);
        }
        else if (!droneIsNear && isThirdPerson)
        {
            SetCameraView(false);
        }
    }
}