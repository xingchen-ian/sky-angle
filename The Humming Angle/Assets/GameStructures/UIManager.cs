using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public TMP_Text healthText;
    public GameObject gameOverPanel;
    public Text gameOverText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void UpdateHealth(float health)
    {
        healthText.text = "Health: " + health;
    }

    public void ShowGameOverScreen(bool hasWon)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = hasWon ? "You Escaped!" : "You Died!";
    }

    public void ResetUI()
    {
        gameOverPanel.SetActive(false);
        UpdateHealth(100); // 假设初始生命值为100
    }
}