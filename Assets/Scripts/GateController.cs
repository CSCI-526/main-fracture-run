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
    private Transform gateParent;
    private BoxCollider gateCollider;
    private Transform arrow;


    // Start is called before the first frame update
    void Start()
    {    
        gateParent = transform.parent;
        gateCollider = gateParent.GetComponent<BoxCollider>();
        arrow = gateParent.Find("Arrow");
        if (arrow == null)
        {
            Debug.LogError("Arrow not found in the gate!");
        }
        leftGateTarget = left_gate.transform.position + new Vector3(0, 0, -openDistance);
        rightGateTarget = right_gate.transform.position + new Vector3(0, 0, openDistance);

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the obstacle is the ball.
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy the obstacle.
            if (arrow != null)
            {
                Debug.Log("Destroy Arrow");
                Destroy(arrow.gameObject);
            }
            if (gateCollider != null) 
            {
                Debug.Log("Disable Gate Collider of: " + gateParent.name);
                gateCollider.enabled = false;
            }
            //Destroy(gameObject);
            gameObject.SetActive(false);

            // Open Gate
            left_gate.transform.position = leftGateTarget;
            right_gate.transform.position = rightGateTarget;
        }
    }



}
