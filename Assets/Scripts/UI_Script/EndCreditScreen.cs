using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndCreditScreen : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button restartButton; // Add restart button reference
    
    [Header("Scene References")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private string gameSceneName = "GameScene"; // Your current game scene
    
    private void Start()
    {
        // Set up button listeners
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
            
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
    }
    
    public void ReturnToMainMenu()
    {
        // Resume normal time before loading a new scene
        Time.timeScale = 1f;
        
        // Load main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
    
    public void RestartGame()
    {
        // Resume normal time before loading a new scene
        Time.timeScale = 1f;
        
        // Reload the current game scene
        SceneManager.LoadScene(gameSceneName);
    }
    
    // Call this to update the final time display
    public void SetFinalTime(string time)
    {
        if (finalTimeText != null)
            finalTimeText.text = "Your Time: " + time;
    }
}