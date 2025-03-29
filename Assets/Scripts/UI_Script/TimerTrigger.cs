using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        StartTimer,
        StopTimer
    }
    
    [Header("Trigger Settings")]
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private bool triggerOnce = true;
    
    private GameTimer gameTimer;
    private bool hasTriggered = false;
    
    private void Start()
    {
        // Find the GameTimer in the scene
        gameTimer = FindObjectOfType<GameTimer>();
        
        if (gameTimer == null)
            Debug.LogError("GameTimer not found in the scene!");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Skip if we should only trigger once and already have
            if (triggerOnce && hasTriggered)
                return;
                
            if (gameTimer != null)
            {
                if (triggerType == TriggerType.StartTimer)
                {
                    gameTimer.StartTimer();
                    Debug.Log("Start Timer Triggered");
                }
                else if (triggerType == TriggerType.StopTimer)
                {
                    gameTimer.StopTimer();
                    Debug.Log("Stop Timer Triggered");
                }
                
                hasTriggered = true;
            }
        }
    }
}