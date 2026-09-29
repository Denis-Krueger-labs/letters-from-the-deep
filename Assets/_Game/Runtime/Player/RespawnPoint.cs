using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDeath playerDeath = other.GetComponentInParent<PlayerDeath>();

        if (playerDeath != null)
        {
            playerDeath.SetRespawnPoint(transform);
            Debug.Log($"Respawn point updated to: {gameObject.name}");
        }
    }
}