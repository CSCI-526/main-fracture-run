using UnityEngine;

public class MoveCube : MonoBehaviour
{
    private float moveSpeed = 1f; 
    private float moveDistance = 8f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        float newPositionZ = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance + startPosition.z;

        transform.position = new Vector3(transform.position.x, transform.position.y, newPositionZ);
    }
}