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
