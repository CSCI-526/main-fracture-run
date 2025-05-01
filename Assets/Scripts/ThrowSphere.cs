using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowSphere : MonoBehaviour
{
    public GameObject spherePrefab; 
    //public float throwForce = 150f; 
    public float throwForce = 6f; 
    public PlayerController playerController;
    // public float speed = 12f;
    // public float angleOffset = 30.0f;

    public Obstacle obstacleManager;

    private bool isBurstMode = false;
    private float burstTimer = 0f;
    private float burstDuration = 5f;
    private float burstCooldown = 0.3f;
    private float burstFireRateTimer = 0f;

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (isBurstMode)
        {
            burstTimer -= Time.deltaTime;
            burstFireRateTimer -= Time.deltaTime;

            if (burstTimer <= 0f)
            {
                isBurstMode = false;
                Debug.Log("Burst mode ended.");
            }
            else if (burstFireRateTimer <= 0f && playerController.ballCount > 0)
            {
                // Vector3 forwardPoint = transform.position + transform.forward * 10f;
                // Throw(forwardPoint);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 targetPoint = ray.GetPoint(50);
                Debug.Log("Throw!");
                Throw(targetPoint);
                burstFireRateTimer = burstCooldown;
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateBurstMode();
        }

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("click mouse!");
            if ( playerController.ballCount >0){
                obstacleManager.ShowFloatingText("-1");
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint = ray.GetPoint(50);
            Debug.Log("Throw!");
            Throw(targetPoint);
        }
    }

    public void ActivateBurstMode()
    {
        if(playerController.ultimate_num > 0) {
            isBurstMode = true;
            burstTimer = burstDuration;
            burstFireRateTimer = 0f;
            Debug.Log("Burst mode activated!");
            playerController.ultimate_num--;
            playerController.ultimateNumText.text = playerController.ultimate_num.ToString();
        }
        
    }

    // void Update()
    // {
    //     if (Time.timeScale == 0f) return;

    //     if (Input.GetMouseButtonDown(0)) 
    //     {
    //         obstacleManager.ShowFloatingText("-1");
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         //RaycastHit hit;
    //         Vector3 targetPoint;

    //         targetPoint = ray.GetPoint(50);
            
    //         // if (Physics.Raycast(ray, out hit))
    //         // {
    //         //     targetPoint = hit.point;
    //         // } else {
    //         //     targetPoint = ray.origin + ray.direction * 10f;
    //         // }

    //         //Vector3 targetPoint = ray.GetPoint(50);
    //         Debug.Log("Throw!");
    //         Throw(targetPoint);
    //     }
    // }

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
            Renderer sphereRenderer = sphere.GetComponent<Renderer>();
            if (isBurstMode) {
                
                if (sphereRenderer != null)
                {
                    // Debug.Log("red");
                    // sphereRenderer.material.color = Color.red; 
                    sphereRenderer.material.color = new Color(0.7f, 0.4f, 0.8f);  // RGB 范围是 0 ~ 1
                }
            }
            else {
                // Debug.Log("grey");
                sphereRenderer.material.color = Color.grey;
            }

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

            if(!isBurstMode)
                playerController.AddBallCount(-1);
        }
        else
        {
            Debug.Log("No more balls left!");
        }
    }

    // public Vector3 SetInitialVelocity(Vector3 throwDirection, float playerSpeed, float speed, float angleOffset)
    // {
    //     Vector3 projection = Vector3.ProjectOnPlane(throwDirection, Vector3.up);
    //     float angle = Vector3.Angle(throwDirection, projection) + angleOffset;
    //     float radian = angle * Mathf.Deg2Rad;
    //     float verticalSpeed = speed * Mathf.Sin(radian);
    //     float horizontalSpeed = speed * Mathf.Cos(radian);
    //     //Vector3 playerDirection = playerController.transform.forward;

    //     Vector3 horizontalVelocity = projection.normalized * horizontalSpeed;
    //     Vector3 throwVelocity = horizontalVelocity + Vector3.up * verticalSpeed;
    //     return throwVelocity;
    // }
}
