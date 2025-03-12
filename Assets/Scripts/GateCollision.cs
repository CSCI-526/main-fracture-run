using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateCollision : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController to access the ball count.

    public Obstacle obstacleManager;
    public CameraShake cameraShake;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the obstacle is the ball.
        if (collision.gameObject.CompareTag("Player"))
        {
            if(playerController != null) {
                obstacleManager.ShowFloatingText("-10");
                playerController.ApplyPenalty(-10);
            }

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
