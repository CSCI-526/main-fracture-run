using UnityEngine;

public class LaserWall : MonoBehaviour
{

   private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[DEBUG] Triggered with: " + other.name + " | Tag: " + other.tag);

        if (!other.CompareTag("Player"))
        {
            Debug.Log("[DEBUG] Not the player, skip.");
            return;
        }

        PlayerController player = other.GetComponent<PlayerController>() ?? other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.ApplyPenalty(-5);
            Debug.Log("✅ Laser hit REAL player: -5 balls");
        }
        else
        {
            Debug.Log("❌ PlayerController NOT found on: " + other.name);
        }
    }


}
