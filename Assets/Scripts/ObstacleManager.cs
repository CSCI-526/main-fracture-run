using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController to access the ball count.

    private void Start()
    {
        // Ensure the PlayerController reference is set at the start.
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference not assigned in Obstacle script.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the obstacle is the ball.
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy the obstacle.
            Destroy(gameObject);

            if (gameObject.CompareTag("Crystal") && playerController != null)
            {
                playerController.AddBallCount(3); // Increase ball count
            }

            // Destroy both Crystals & Obstacles when hit
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Obstacle"))
        {
            if (playerController != null)
            {
                playerController.ApplyPenalty(); // Call a penalty function in PlayerController
            }

            Destroy(gameObject); // Remove the obstacle after penalty is applied
        }
    }
}

