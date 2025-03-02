using UnityEngine;
using StarterAssets; // 引入 Starter Assets

public class PlayerStateManager : MonoBehaviour
{
    private ThirdPersonController controller; // 引用 Starter Assets 的角色控制器
    private bool isThirdPerson = true;

    private void Start()
    {
        controller = GetComponent<ThirdPersonController>(); // 获取角色控制器
    }

    private void Update()
    {
        /*
        // 无人机接近时切换视角
        if (DroneManager.Instance.IsDroneNearby())
        {
            SetThirdPersonView(false);
        }
        else
        {
            SetThirdPersonView(true);
        }
        */
    }

    private void SetThirdPersonView(bool enable)
    {
        if (isThirdPerson == enable) return;
        isThirdPerson = enable;

        // 让 CameraManager 负责切换摄像机
        CameraManager.Instance.SetCameraView(enable);
    }
}