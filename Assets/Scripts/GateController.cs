using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{

    public GameObject left_gate;
    public GameObject right_gate;
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
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy the obstacle.
            Destroy(gameObject);
            // Open Gate
            left_gate.transform.position = new Vector3(left_gate.transform.position.x, left_gate.transform.position.y, left_gate.transform.position.z - 5);
            right_gate.transform.position = new Vector3(right_gate.transform.position.x, right_gate.transform.position.y, right_gate.transform.position.z + 5);
        }
    }
}
