using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private DeathScreen deathScreen;

    private void Start()
    {
        // Find the DeathScreen script in the scene
        deathScreen = FindObjectOfType<DeathScreen>();
        
        if (deathScreen == null)
            Debug.LogError("DeathScreen not found in the scene!");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Freeze player movement
            FreezePlayer(other.gameObject);
            
            // Show death screen
            if (deathScreen != null)
                deathScreen.ShowDeathScreen();
        }
    }
    
    private void FreezePlayer(GameObject player)
    {
        // Option 1: Disable player controller
        var playerController = player.GetComponent<CharacterController>();
        if (playerController != null)
            playerController.enabled = false;
            
        // Option 2: Freeze rigidbody if the player uses physics
        var rb = player.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
            
        // Option 3: Disable all player input scripts
        var playerScripts = player.GetComponents<MonoBehaviour>();
        foreach (var script in playerScripts)
        {
            // Skip disabling this script and essential components
            if (script is KillTrigger || script is DeathScreen)
                continue;
                
            script.enabled = false;
        }
    }
}