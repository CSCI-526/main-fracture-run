using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{

    public GameObject left_gate;
    public GameObject right_gate;

    public float openDistance = 5f;

    private Vector3 leftGateTarget;
    private Vector3 rightGateTarget;


    // Start is called before the first frame update
    void Start()
    {    
        leftGateTarget = left_gate.transform.position + new Vector3(0, 0, -openDistance);
        rightGateTarget = right_gate.transform.position + new Vector3(0, 0, openDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the obstacle is the ball.
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy the obstacle.
            Destroy(gameObject);
            // Open Gate
            left_gate.transform.position = leftGateTarget;
            right_gate.transform.position = rightGateTarget;
        }
    }


}
