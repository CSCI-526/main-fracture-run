using UnityEngine;

public class LaserWallController : MonoBehaviour
{
    public Transform[] laserBeams;
    public int minGap = 1;
    public int maxGap = 2;

    void Start()
    {
        int total = laserBeams.Length;
        int gapCount = Random.Range(minGap, maxGap + 1);
        int[] gapIndices = new int[gapCount];

        for (int i = 0; i < gapCount; i++)
        {
            int idx;
            do
            {
                idx = Random.Range(0, total);
            } while (System.Array.IndexOf(gapIndices, idx) != -1);

            gapIndices[i] = idx;
        }

        for (int i = 0; i < total; i++)
        {
            bool isGap = System.Array.IndexOf(gapIndices, i) != -1;
            
            MeshRenderer mr = laserBeams[i].GetComponent<MeshRenderer>();
            if (mr != null)
                mr.enabled = !isGap;
        }
    }
}
