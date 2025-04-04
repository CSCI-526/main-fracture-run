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
    private bool isSpawning = false;
    private int index;
    private int poolSize;
    


    void Start()
    {
        endCube = GameObject.Find("EndCube").transform;
        objectPrefabs = Resources.LoadAll<GameObject>("GameScenes");
        poolSize = objectPrefabs.Length;
        if (poolSize == 0)
        {
            Debug.LogError("No prefabs found in Resources/GameScenes!");
            return;
        }
        index = poolSize - 1;

        for (int i = 0; i < poolSize; i++)
        {
            objectPrefabs[i] = Instantiate(objectPrefabs[i]);
            objectPrefabs[i].SetActive(false);
            Debug.Log($"Start: Prefab {objectPrefabs[i]} instantiated at {objectPrefabs[i].transform.position}");
        }
    }

    void Update()
    {
        // Check if player is close to EndCube to spawn the next object (using x-axis)
        if (!isSpawning && player.transform.position.x < endCube.transform.position.x + spawnDistance)
        {
            Debug.Log("endCube position: " + endCube.transform.position);
            //float endCubeLength = Mathf.Abs(endCube.GetComponent<Renderer>().bounds.size.x);
            float endCubeLength = endCube.lossyScale.z;
            Debug.Log("endCubeLength: " + endCubeLength);
            Vector3 spawnPosition = endCube.transform.position + new Vector3(-endCubeLength, 0, 0);
            SpawnObject(spawnPosition);
        }
        else if (player.transform.position.x >= endCube.transform.position.x + spawnDistance)
        {
            isSpawning = false;
        }

        if (prevPrefab != null && player.transform.position.x < startCube.position.x && currPrefab != prevPrefab)
        {
            prevPrefab.SetActive(false);
        }
    }

    public void SpawnObject(Vector3 spawnPosition)
    {
        Debug.Log("Call SpawnObject method");
        isSpawning = true;

        if (poolSize == 0)
        {
            Debug.LogError("No prefabs available for spawning!");
            return;
        }

        if (currPrefab != null)
        {
            prevPrefab = currPrefab;
        }

        // Shuffle the array of prefabs one by one
        int randomIndex = Random.Range(0, index + 1);
        Debug.Log("Random index: " + randomIndex);
        swap(objectPrefabs, index, randomIndex);
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
        currPrefab.SetActive(true);
        SetAllChildrenActive(currPrefab.transform, true);
    
        Obstacle[] obstacles = currPrefab.GetComponentsInChildren<Obstacle>();
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.playerController = playerController;
            obstacle.canvasTransform = canvasTransform; // 赋值 Canvas Transform
            Debug.Log($"Assigned PlayerController to {obstacle.gameObject.name}");
        }

        Debug.Log($"Object {currPrefab.name} spawn position {spawnPosition} activated at position {currPrefab.transform.position}");    
        
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = poolSize - 1;
        }

    }

    private void swap(GameObject[] arr, int i, int j)
    {
        GameObject temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    private void SetAllChildrenActive(Transform parent, bool isActive)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(isActive);
            Debug.Log($"Set {child.gameObject.name} active: {isActive}");
            // Recursively activate children
            SetAllChildrenActive(child, isActive);
        }
    }

}
