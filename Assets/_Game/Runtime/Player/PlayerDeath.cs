using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rigidbodyPlayer;
    private Vector3 respawnPosition;

    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();

        // Starting position is the default respawn point.
        respawnPosition = transform.position;
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        if (newRespawnPoint == null)
        {
            return;
        }

        respawnPosition = newRespawnPoint.position;

        Debug.Log($"Respawn point updated to: {respawnPosition}");
    }

    public void Die()
    {
        Debug.Log($"Player died. Respawning at: {respawnPosition}");

        if (rigidbodyPlayer != null)
        {
            rigidbodyPlayer.linearVelocity = Vector2.zero;
            rigidbodyPlayer.angularVelocity = 0f;
        }

        transform.position = respawnPosition;

        Physics2D.SyncTransforms();
    }
}