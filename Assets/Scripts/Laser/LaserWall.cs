using UnityEngine;

public class LaserWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplyPenalty(-5); // 扣5个 ball
                Debug.Log("Laser hit! -5 balls");
            }
        }
    }
}
