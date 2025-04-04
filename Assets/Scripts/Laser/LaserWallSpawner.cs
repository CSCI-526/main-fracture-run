using UnityEngine;
using System.Collections;

public class LaserWallSpawner : MonoBehaviour
{
    public GameObject laserWallPrefab;
    public Vector3 localOffset = new Vector3(0, 0, 0);
    public float spawnInterval = 3f;
    public float moveSpeed = 5f;
    public float lifeTime = 7f;  

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;

            Vector3 spawnPos = transform.position + localOffset;
            GameObject wall = Instantiate(laserWallPrefab, spawnPos, Quaternion.identity);

            // ✅ 自动为所有 Obstacle 脚本赋值 playerController 引用
            PlayerController player = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();
            if (player != null)
            {
                foreach (Obstacle obstacle in wall.GetComponentsInChildren<Obstacle>())
                {
                    obstacle.playerController = player;
                }
            }
            else
            {
                Debug.LogError("Player with tag 'Player' not found!");
            }

            StartCoroutine(MoveAndDestroy(wall));
        }
    }

    IEnumerator MoveAndDestroy(GameObject wall)
    {
        float t = 0f;
        while (wall != null && t < lifeTime)
        {
            wall.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }

        if (wall != null)
        {
            Destroy(wall);
        }
    }
}
