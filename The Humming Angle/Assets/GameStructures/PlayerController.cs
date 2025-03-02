using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float health = 100f;
    public float crouchExposureReduction = 0.5f; // 蹲伏降低暴露值
    
    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isCrouching = false;
    
    public bool HasEscaped { get; private set; } = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        ResetPlayer();
    }

    private void Update()
    {
        HandleMovement();
        HandleCrouch();
    }

    /// <summary>
    /// 处理玩家移动
    /// </summary>
    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 处理蹲伏状态
    /// </summary>
    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            moveSpeed = isCrouching ? moveSpeed * 0.5f : moveSpeed * 2f;
        }
    }

    /// <summary>
    /// 受伤逻辑
    /// </summary>
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.EndGame(false); // 通知 GameManager 失败
        }
    }

    /// <summary>
    /// 逃脱游戏
    /// </summary>
    public void Escape()
    {
        HasEscaped = true;
        GameManager.Instance.EndGame(true); // 通知 GameManager 胜利
    }

    /// <summary>
    /// 重置玩家状态
    /// </summary>
    public void ResetPlayer()
    {
        health = 100f;
        HasEscaped = false;
        transform.position = Vector3.zero; // 这里可以调整为实际的出生点
        isCrouching = false;
        moveSpeed = 5f;
    }
}