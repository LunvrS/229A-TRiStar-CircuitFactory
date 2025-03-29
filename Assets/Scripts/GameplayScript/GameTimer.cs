using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timerText;
    
    [Header("End Screen Reference")]
    [SerializeField] private GameObject endCreditScreen;
    
    private float timer = 0f;
    private bool isTimerRunning = false;
    
    private void Start()
    {
        // Hide end credit screen at start
        if (endCreditScreen != null)
            endCreditScreen.SetActive(false);
            
        // Initialize timer display
        UpdateTimerDisplay();
    }
    
    private void Update()
    {
        if (isTimerRunning)
        {
            // Increment timer
            timer += Time.deltaTime;
            
            // Update UI
            UpdateTimerDisplay();
        }
    }
    
    public void StartTimer()
    {
        isTimerRunning = true;
        timer = 0f;
        Debug.Log("Timer started!");
    }
    
    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("Timer stopped! Final time: " + timer.ToString("F2") + " seconds");
    
        // Unlock and show cursor immediately
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    
        // Freeze the game
        Time.timeScale = 0f;
    
        // Find and freeze player explicitly
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Disable movement components
            var controller = player.GetComponent<CharacterController>();
            if (controller != null)
                controller.enabled = false;
            
            var rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.isKinematic = true;
            }
        
            // Disable all movement scripts
            var scripts = player.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                // Skip essential components
                if (script is GameTimer)
                    continue;
                
                script.enabled = false;
            }
        }
    
        // Show end credit screen
        if (endCreditScreen != null)
        {
            // Format time for display
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            int milliseconds = Mathf.FloorToInt((timer * 100f) % 100f);
            string timeText = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        
            // Find text component in end credits screen
            var finalTimeText = endCreditScreen.GetComponentInChildren<TextMeshProUGUI>();
            if (finalTimeText != null)
                finalTimeText.text = "Your Time: " + timeText;
            
            // Show the screen
            endCreditScreen.SetActive(true);
        }
    }
    
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // Format time as minutes:seconds.milliseconds
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            int milliseconds = Mathf.FloorToInt((timer * 100f) % 100f);
            
            timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }
    }
}