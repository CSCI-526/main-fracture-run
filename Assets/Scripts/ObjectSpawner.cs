using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // public GameObject objectPrefab; // Prefab to spawn
    public Transform player;        // Reference to the player
    public float spawnDistance = 10f; // Distance to trigger next spawn
    public PlayerController playerController; // Reference to PlayerController
    public Transform canvasTransform; 

    public GameObject lastSpawnedObject;
    private GameObject[] objectPrefabs;

    


    void Start()
    {
        // Spawn the first object at the initial position
        // if (objectPrefab != null)
        // {
        //     lastSpawnedObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
        // }
        // else
        // {
        //     Debug.LogError("Prefab is not assigned!");
        // }
        objectPrefabs = Resources.LoadAll<GameObject>("GameScenes");
        if (objectPrefabs.Length == 0)
        {
            Debug.LogError("No prefabs found in Resources/GameScenes!");
            return;
        }
    }

    void Update()
    {
        if (lastSpawnedObject == null)
        {
            Debug.LogError("Last spawned object is missing!");
            return;
        }

        Transform endCube = lastSpawnedObject.transform.Find("EndCube");
        if (endCube == null)
        {
            Debug.LogError("EndCube not found in the prefab!");
            return;
        }
        // Debug.Log("endCube position: " + endCube.transform.position);
        // Debug.Log("player position: " + player.transform.position);
        // Check if player is close to EndCube to spawn the next object (using x-axis)
        if (player.transform.position.x < endCube.transform.position.x + spawnDistance)
        {
            Debug.Log("endCube position: " + endCube.transform.position);
            float endCubeScaleX = endCube.lossyScale.z;
            // newObject.transform.position += new Vector3(endCubeScaleX, 0, 0);
            SpawnObject(endCube.transform.position + new Vector3(-endCubeScaleX, 0, 0));
        }
    }

    public void SpawnObject(Vector3 spawnPosition)
    {
        if (objectPrefabs.Length == 0)
        {
            Debug.LogError("No prefabs available for spawning!");
            return;
        }

        GameObject randomPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
        GameObject newObject = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

        Transform startCube = newObject.transform.Find("StartCube");
        if (startCube != null)
        {
            Vector3 offset = newObject.transform.position - startCube.transform.position;
            newObject.transform.position = spawnPosition + offset;
        }

    
        Obstacle[] obstacles = newObject.GetComponentsInChildren<Obstacle>();
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.playerController = playerController;
            obstacle.canvasTransform = canvasTransform; // 赋值 Canvas Transform
            Debug.Log($"Assigned PlayerController to {obstacle.gameObject.name}");
        }

        lastSpawnedObject = newObject;
        Debug.Log($"Object spawned: {randomPrefab.name} at {spawnPosition}");
    }

}
