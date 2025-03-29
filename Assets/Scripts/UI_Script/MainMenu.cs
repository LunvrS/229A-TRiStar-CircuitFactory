using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject creditsPanel;
    
    [Header("Game Settings")]
    [SerializeField] private string gameSceneName = "GameScene"; // Change to your actual game scene name

    private void Start()
    {
        // Initialize button listeners
        startButton.onClick.AddListener(StartGame);
        creditsButton.onClick.AddListener(ShowCredits);
            
        // Make sure main panel is active and credits is not
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    
    public void ShowCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    
    public void BackToMainMenu()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}