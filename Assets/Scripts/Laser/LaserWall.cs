using UnityEngine;
using TMPro;


public class LaserWall : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController to access the ball count.
    public CameraShake cameraShake;

    public GameObject floatingTextPrefab; 
    public Transform canvasTransform; 
    private bool penaltyApplied = false;

    private void Start()
    {
        // Ensure the PlayerController reference is set at the start.
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference not assigned in Obstacle script.");
        }
        cameraShake = FindObjectOfType<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogError("CameraShake script not found in the scene!");
        }
    }

    public void ShowFloatingText(string point)
    {
        
        if (floatingTextPrefab && canvasTransform)
        {
            GameObject text = Instantiate(floatingTextPrefab, canvasTransform);
            text.GetComponent<TextMeshProUGUI>().text = point;
            if(int.Parse(point) > 0) {
                text.GetComponent<TextMeshProUGUI>().color = Color.green;
            } 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the obstacle is the ball.
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Disable its BoxCollider.
            BoxCollider collider = GetComponent<BoxCollider>();
            if (collider != null)
            {
                Debug.Log("Disable Obstacle BoxCollider of: " + gameObject.name);
                collider.enabled = false;
            }
            else 
            {
                Debug.LogError("BoxCollider not found in the obstacle!");
            }
            // Destroy the obstacle.
            // Destroy(gameObject);

            // if (gameObject.CompareTag("Crystal") && playerController != null)
            // {
            //     ShowFloatingText("+3");
            //     playerController.AddBallCount(3); // Increase ball count
            // }

            // if (gameObject.CompareTag("Bonus") && playerController != null)
            // {
            //     ShowFloatingText("+8");
            //     playerController.AddBallCount(8); // Increase ball count
            // }

            // Destroy both Crystals & Obstacles when hit
            //Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Obstacle"))
        {
            if (playerController != null)
            {
                Debug.Log("Player hit the obstacle: " + gameObject.name);
                ShowFloatingText("-5");
                playerController.ApplyPenalty(-5); // Call a penalty function in PlayerController
            }
            
            
            if (cameraShake != null)
            {
                Debug.Log("Shake the camera because player hit the obstacle: " + gameObject.name);
                cameraShake.Shake();
            }

            Destroy(gameObject); // Remove the obstacle after penalty is applied
        }

        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Fan"))
        {
            Debug.Log("Player collides with Fan!");
            if (!penaltyApplied)
            {
                if (playerController != null)
                {
                    penaltyApplied = true;
                    ShowFloatingText("-5");
                    playerController.ApplyPenalty(-5); // Call a penalty function in PlayerController
                }      
            
                if (cameraShake != null)
                {
                    Debug.Log("Shake the camera!");
                    cameraShake.Shake();
                }

                Destroy(gameObject); // Remove the obstacle after penalty is applied
            }

        } 

    }
}

