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


    public GameObject transparentObject; // Reference to the transparent object
    public Image uiImage; // Reference to the UI image
    public Canvas uiCanvas; // Reference to the Canvas

    private void Start()
    {
        // Instantiate the transparent object behind the gate
        transparentObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        transparentObject.transform.position = transform.position + new Vector3(0, 0, 1);
        transparentObject.transform.localScale = new Vector3(1, 1, 1); // Adjust size as needed
        transparentObject.GetComponent<Renderer>().material = new Material(Shader.Find("Transparent/Diffuse"));
        transparentObject.GetComponent<Collider>().isTrigger = true;
    }

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
