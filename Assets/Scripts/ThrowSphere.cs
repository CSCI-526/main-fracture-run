using UnityEngine;

public class ThrowSphere : MonoBehaviour
{
    public GameObject spherePrefab; 
    public float throwForce = 10f; 
    public PlayerController playerController;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Throw();
        }
    }

    void Throw()
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

            
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;

            // create sphere
            GameObject sphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);

            Debug.Log("sphere created：" + sphere.name);

            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("error: sphere has no rigid body component！");
                return;
            }

        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint;

            targetPoint = ray.GetPoint(50);
 
            Vector3 throwDirection = (targetPoint - spawnPosition).normalized;

            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

            //Debug.Log("球体朝 " + targetPoint + " 抛出！");
            playerController.AddBallCount(-1);
        }
        else
        {
            Debug.Log("No more balls left!");
        }
    }
}
