using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // public GameObject objectPrefab; // Prefab to spawn
    public Transform player;        // Reference to the player
    public float spawnDistance = 10f; // Distance to trigger next spawn
    public PlayerController playerController; // Reference to PlayerController
    public Transform canvasTransform; 
    public GameObject currPrefab;

    private GameObject prevPrefab;
    private GameObject[] objectPrefabs;
    private Transform startCube;
    private Transform endCube;
    private Vector3 offset;
    private int index;
    private int poolSize;

    void Start()
    {
        endCube = GameObject.Find("EndCube").transform;
        //objectPrefabs = Resources.LoadAll<GameObject>("GameScenes");
        objectPrefabs = new GameObject[7];
        objectPrefabs[0] = Resources.Load<GameObject>("GameScenes/JYScene");
        objectPrefabs[1] = Resources.Load<GameObject>("GameScenes/TwoBranchScene");
        objectPrefabs[2] = Resources.Load<GameObject>("GameScenes/ElsaScene");
        objectPrefabs[3] = Resources.Load<GameObject>("GameScenes/SerenaScene");
        objectPrefabs[4] = Resources.Load<GameObject>("GameScenes/JingxuanScene");
        objectPrefabs[5] = Resources.Load<GameObject>("GameScenes/ShujieScene");
        objectPrefabs[6] = Resources.Load<GameObject>("GameScenes/JiayuScene");

        index = 0;

    }

    void Update()
    {
        // Check if player is close to EndCube to spawn the next object (using x-axis)
        if (player.transform.position.x < endCube.transform.position.x + spawnDistance)
        {
            Debug.Log("endCube position: " + endCube.transform.position);
            //float endCubeLength = Mathf.Abs(endCube.GetComponent<Renderer>().bounds.size.x);
            float endCubeLength = endCube.lossyScale.z;
            Debug.Log("endCubeLength: " + endCubeLength);
            Vector3 spawnPosition = endCube.transform.position + new Vector3(-endCubeLength, 0, 0);
            SpawnObject(spawnPosition);
        }

        if (prevPrefab != null && player.transform.position.x < startCube.position.x && currPrefab != prevPrefab)
        {
            Destroy(prevPrefab);
        }
    }

    public void SpawnObject(Vector3 spawnPosition)
    {
        Debug.Log("Call SpawnObject method");

        if (currPrefab != null)
        {
            prevPrefab = currPrefab;
        }

        currPrefab = objectPrefabs[index];

        endCube = currPrefab.transform.Find("EndCube");
        if (endCube == null)
        {
            Debug.LogError("EndCube not found in the prefab!");
            return;
        }

        startCube = currPrefab.transform.Find("StartCube");
        offset = currPrefab.transform.position - startCube.transform.position;
        Debug.Log($"{currPrefab.name} offset: " + offset);

        currPrefab.transform.position = spawnPosition + offset;

        currPrefab = Instantiate(currPrefab);
    
        Obstacle[] obstacles = currPrefab.GetComponentsInChildren<Obstacle>();
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.playerController = playerController;
            obstacle.canvasTransform = canvasTransform; // 赋值 Canvas Transform
            Debug.Log($"Assigned PlayerController to {obstacle.gameObject.name}");
        }

        Debug.Log($"Object {currPrefab.name} spawn position {spawnPosition} activated at position {currPrefab.transform.position}");    
        
        if (index < 6)
        {
            index++;
        }
        else
        {
            index = 0;
        }

    }

}
