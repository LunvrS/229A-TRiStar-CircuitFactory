using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovementUI : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private GameObject player;
    
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI directionText;
    
    // Component references
    private Rigidbody playerRigidbody;
    private CharacterController playerController;
    
    // Last position for calculating velocity if needed
    private Vector3 lastPosition;
    private Vector3 velocity;
    
    private void Start()
    {
        // If player is not assigned, try to find it
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
            
        if (player == null)
        {
            Debug.LogError("Player not found! Please assign the player GameObject.");
            enabled = false;
            return;
        }
        
        // Try to get movement components
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerController = player.GetComponent<CharacterController>();
        
        lastPosition = player.transform.position;
    }
    
    private void Update()
    {
        // Calculate current velocity
        Vector3 currentVelocity = GetPlayerVelocity();
        
        // Calculate speed (magnitude of velocity)
        float speed = currentVelocity.magnitude;
        
        // Update UI texts
        if (speedText != null)
            speedText.text = $"Speed: {speed:F2} units/s";
            
        if (directionText != null)
            directionText.text = $"Direction: X: {currentVelocity.x:F2}, Y: {currentVelocity.y:F2}, Z: {currentVelocity.z:F2}";
    }
    
    private Vector3 GetPlayerVelocity()
    {
        // Try different methods to get velocity based on available components
        if (playerRigidbody != null)
        {
            // Use Rigidbody velocity
            return playerRigidbody.linearVelocity;
        }
        else if (playerController != null)
        {
            // Use CharacterController velocity
            return playerController.velocity;
        }
        else
        {
            // Manual calculation based on position change
            Vector3 currentPosition = player.transform.position;
            velocity = (currentPosition - lastPosition) / Time.deltaTime;
            lastPosition = currentPosition;
            return velocity;
        }
    }
}