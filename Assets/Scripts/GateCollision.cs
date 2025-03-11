using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateCollision : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController to access the ball count.

    public Obstacle obstacleManager;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the obstacle is the ball.
        if (collision.gameObject.CompareTag("Player"))
        {
            // game over
            if(playerController != null) {
                //playerController.ShowGameOver();
                obstacleManager.ShowFloatingText("-10");
                playerController.ApplyPenalty(-10);
            }
            Destroy(gameObject);
        }
    }
}
