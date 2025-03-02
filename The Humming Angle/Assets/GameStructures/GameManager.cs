using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 单例模式
    
    public event Action OnGameStart;  // 游戏开始事件
    public event Action<bool> OnGameOver; // 游戏结束事件（参数：是否胜利）

    [Header("Game Components")]
    public PlayerController player;  // 玩家控制
    public DroneManager droneManager;  // 无人机管理
    public UIManager uiManager;  // UI 管理
    public MapManager mapManager;  // 地图管理（未来可扩展）
    
    public bool isGameActive { get; private set; } = false;  // 游戏是否进行中

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (!isGameActive) return;
        
        CheckGameStatus();
    }

    /// <summary>
    /// 初始化游戏
    /// </summary>
    public void InitializeGame()
    {
        isGameActive = true;
        
        mapManager?.GenerateMap(); // 生成地图（如果有）
        player?.ResetPlayer(); // 复位玩家状态
        droneManager?.ResetDrones(); // 复位无人机
        uiManager?.ResetUI(); // 复位 UI
        
        OnGameStart?.Invoke(); // 触发游戏开始事件
    }

    /// <summary>
    /// 监控游戏状态
    /// </summary>
    private void CheckGameStatus()
    {
        if (player.health <= 0) // 玩家死亡
        {
            EndGame(false);
        }
        else if (player.HasEscaped) // 玩家逃脱成功
        {
            EndGame(true);
        }
    }

    /// <summary>
    /// 结束游戏
    /// </summary>
    /// <param name="hasWon">是否胜利</param>
    public void EndGame(bool hasWon)
    {
        isGameActive = false;
        
        uiManager?.ShowGameOverScreen(hasWon); // 调用 UIManager 显示游戏结束界面
        OnGameOver?.Invoke(hasWon); // 触发游戏结束事件
    }
}