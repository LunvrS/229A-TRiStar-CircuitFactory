using UnityEngine;

public class CircuitBoardController : MonoBehaviour
{
    private bool isHolding = false;
    private Transform player;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float rotationSpeed = 5f;

    void Start()
    {
        player = Camera.main.transform; // Assuming the player "looks" at the board
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding)
                PickUp();
            else
                PutDown();
        }

        if (isHolding && Input.GetMouseButton(0)) // Left mouse button to rotate
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;
            
            transform.Rotate(player.up, -rotX, Space.World); // Horizontal rotation
            transform.Rotate(player.right, rotY, Space.World); // Vertical rotation
        }
    }

    void PickUp()
    {
        isHolding = true;
        transform.position = player.position + player.forward * 1.5f; // Moves board in front of camera
        transform.LookAt(player); // Adjust orientation
    }

    void PutDown()
    {
        isHolding = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}