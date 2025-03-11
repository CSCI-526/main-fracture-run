using UnityEngine;

public class ThrowSphere : MonoBehaviour
{
    public GameObject spherePrefab; 
    //public float throwForce = 150f; 
    public float throwForce = 6f; 
    public PlayerController playerController;
    // public float speed = 12f;
    // public float angleOffset = 30.0f;

    public Obstacle obstacleManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            obstacleManager.ShowFloatingText("-1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            Vector3 targetPoint;

            targetPoint = ray.GetPoint(50);
            
            // if (Physics.Raycast(ray, out hit))
            // {
            //     targetPoint = hit.point;
            // } else {
            //     targetPoint = ray.origin + ray.direction * 10f;
            // }

            //Vector3 targetPoint = ray.GetPoint(50);
            Debug.Log("Throw!");
            Throw(targetPoint);
        }
    }

    void Throw(Vector3 targetPoint)
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference is missing!");
            return;
        }
        Debug.Log("create sphere...");

        if (playerController.ballCount > 0)
        {
            Debug.Log("Creating sphere...");
            
            if (spherePrefab == null)
            {
                Debug.LogError("error：Sphere Prefab no value");
                return;
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // get player scale
            Vector3 playerScale = player.transform.localScale;
            Vector3 offset = new Vector3(0, playerScale.y + 0.5f, 0);
            Vector3 initialPosition = player.transform.position + offset;
            //Vector3 initialPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;

            // create sphere
            GameObject sphere = Instantiate(spherePrefab, initialPosition, Quaternion.identity);
            Destroy(sphere, 3f); // Destroy the sphere after 5 seconds

            Debug.Log("sphere created：" + sphere.name);

            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("error: sphere has no rigid body component！");
                return;
            }
            rb.mass = 0.1f;

            Vector3 throwDirection = (targetPoint - initialPosition).normalized;
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            // float playerSpeed = playerController.speed;

            // SphereController sphereScript = sphere.GetComponent<SphereController>();
            // if (sphereScript != null)
            // {
            //     Vector3 throwDirection = targetPoint - initialPosition;
            //     rb.velocity = SetInitialVelocity(throwDirection, playerSpeed, speed, angleOffset);
            // }

            Collider sphereCollider = sphere.GetComponent<Collider>();
            Collider playerCollider = playerController.GetComponent<Collider>();

            if (sphereCollider != null && playerCollider != null)
            {
                Physics.IgnoreCollision(sphereCollider, playerCollider);
            }

            //rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            //rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
            //Debug.Log("球体朝 " + targetPoint + " 抛出！");


            playerController.AddBallCount(-1);
        }
        else
        {
            Debug.Log("No more balls left!");
        }
    }

    public Vector3 SetInitialVelocity(Vector3 throwDirection, float playerSpeed, float speed, float angleOffset)
    {
        Vector3 projection = Vector3.ProjectOnPlane(throwDirection, Vector3.up);
        float angle = Vector3.Angle(throwDirection, projection) + angleOffset;
        float radian = angle * Mathf.Deg2Rad;
        float verticalSpeed = speed * Mathf.Sin(radian);
        float horizontalSpeed = speed * Mathf.Cos(radian);
        //Vector3 playerDirection = playerController.transform.forward;

        Vector3 horizontalVelocity = projection.normalized * horizontalSpeed;
        Vector3 throwVelocity = horizontalVelocity + Vector3.up * verticalSpeed;
        return throwVelocity;
    }
}
