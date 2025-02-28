using UnityEngine;

public class ArrowHint : MonoBehaviour
{
    public float speed = 2f;
    public float amount = 0.1f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * speed) * amount, 0);
    }
}