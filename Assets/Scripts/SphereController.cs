using UnityEngine;
using TMPro;

public class SphereController : MonoBehaviour
{
    private Rigidbody rb;
    private bool Landed = false; 
    private Vector3 horizontalVelocity; 
    public float timeLapse = 2f;
    public float destroyAfterThrown = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;

        Invoke("DestroySphere", destroyAfterThrown);
    }

    void Update()
    {
        // After landing, maintain horizontal velocity
        if (Landed)
        {
            rb.velocity = horizontalVelocity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!Landed && collision.gameObject.CompareTag("Ground"))
        {
            Landed = true;

            // Reduce bounciness so it stops bouncing
            rb.drag = 0.5f;  
            rb.angularDrag = 0.5f; 
            rb.useGravity = true; 

            // Destroy the sphere in a time lapse
            Invoke("DestroySphere", timeLapse);

        }

        if (collision.gameObject.CompareTag("Crystal") 
        || collision.gameObject.CompareTag("Bonus")
        || collision.gameObject.CompareTag("Obstacle")
        || collision.gameObject.CompareTag("Fan")
        || collision.gameObject.CompareTag("Key"))
        {

            //DestroySphere();
            Destroy(gameObject);
            Destroy(collision.gameObject); // Destroy the colliding object
        }
    }

    void DestroySphere()
    {
        Destroy(gameObject);
    }
}
