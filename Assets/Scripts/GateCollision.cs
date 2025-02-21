using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCollision : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController to access the ball count.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the obstacle is the ball.
        if (collision.gameObject.CompareTag("Player"))
        {
            // game over
            if(playerController != null) {
                playerController.ShowGameOver();
            }
        }
    }
}
