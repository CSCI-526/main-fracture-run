using UnityEngine;

public class ThrowSphere : MonoBehaviour
{
    public GameObject spherePrefab; 
    //public float throwForce = 150f; 
    //public float throwForce = 50f; 
    public PlayerController playerController;
    public float initSpeed = 10f;
    public float initAngle = 60.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint = ray.GetPoint(50);
            Throw(targetPoint);
/*
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                Vector3 targetPoint = hit.point; 
                Throw(targetPoint); 
            }
*/
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
            Vector3 initialPosition = player.transform.position;
            //Vector3 initialPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;

            // create sphere
            GameObject sphere = Instantiate(spherePrefab, initialPosition, Quaternion.identity);

            Debug.Log("sphere created：" + sphere.name);

            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("error: sphere has no rigid body component！");
                return;
            }
            rb.mass = 0.1f;

            float playerSpeed = playerController.speed;

            SphereController sphereScript = sphere.GetComponent<SphereController>();
            if (sphereScript != null)
            {
                Vector3 throwDirection = targetPoint - initialPosition;
                throwDirection.y = 0;
                rb.velocity = SetInitialVelocity(throwDirection, playerSpeed, initSpeed, initAngle);
            }

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

    public Vector3 SetInitialVelocity(Vector3 throwDirection, float playerSpeed, float speed, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float verticalSpeed = speed * Mathf.Sin(radian);
        float horizontalSpeed = speed * Mathf.Cos(radian) + playerSpeed;

        Vector3 horizontalVelocity = throwDirection.normalized * horizontalSpeed;
        Vector3 throwVelocity = horizontalVelocity + Vector3.up * verticalSpeed;
        return throwVelocity;
    }
}
