using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject deathScreenPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    
    [Header("Scene Names")]
    [SerializeField] private string mainMenuScene = "MainMenu";
    [SerializeField] private string gameSceneName = "GameScene"; // Your current game scene
    
    // Store the initial cursor state to restore later if needed
    private bool initialCursorVisible;
    private CursorLockMode initialCursorLockMode;

    private void Start()
    {
        // Hide death screen at start
        deathScreenPanel.SetActive(false);
        
        // Store initial cursor state
        initialCursorVisible = Cursor.visible;
        initialCursorLockMode = Cursor.lockState;
        
        // Setup button listeners
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    // Call this method when the player dies
    public void ShowDeathScreen()
    {
        // Pause the game
        Time.timeScale = 0f;
        
        // Show cursor and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        // Show the death screen
        deathScreenPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // Resume normal time
        Time.timeScale = 1f;
        
        // Reset cursor to gameplay state if needed
        ResetCursorState();
        
        // Reload the current scene
        SceneManager.LoadScene(gameSceneName);
    }

    public void ReturnToMainMenu()
    {
        // Resume normal time
        Time.timeScale = 1f;
        
        // Keep cursor visible for menu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        // Load the main menu
        SceneManager.LoadScene(mainMenuScene);
    }
    
    private void ResetCursorState()
    {
        // This resets the cursor to its initial state when the game starts
        // Only needed if returning to gameplay
        Cursor.visible = initialCursorVisible;
        Cursor.lockState = initialCursorLockMode;
    }
}