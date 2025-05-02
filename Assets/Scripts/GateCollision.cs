using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GateCollision : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController to access the ball count.

    public Obstacle obstacleManager;
    public CameraShake cameraShake;

    private void Start()
{   
    if (playerController == null)
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
            Debug.LogError("PlayerController not found!");
    }

    if (obstacleManager == null)
    {
        obstacleManager = FindObjectOfType<Obstacle>();
        if (obstacleManager == null)
            Debug.LogError("ObstacleManager not found!");
    }

}

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the obstacle is the ball.
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit the gate: " + gameObject.name);
            Debug.LogError("PlayerController is null:" + playerController == null);
            
            if(playerController != null) {
                Debug.Log("Decrement 10 balls because of gate collision.");
                obstacleManager.ShowFloatingText("-10");
                playerController.ApplyPenalty(-10);
            }
/*          
            Debug.Log("Decrement 10 balls because of gate collision.");
            obstacleManager.ShowFloatingText("-10");
            playerController.ApplyPenalty(-10);
*/
            cameraShake = FindObjectOfType<CameraShake>();
            if (cameraShake == null)
            {
                Debug.LogError("CameraShake script not found in the scene!");
            }

            if (cameraShake != null)
            {
                Debug.Log("Shake the camera when player hits the gate: " + gameObject.name);
                cameraShake.Shake();
            }

            Destroy(gameObject);
        }
    
    }
}
