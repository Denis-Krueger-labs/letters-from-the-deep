using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private Rigidbody2D rigidbodyPlayer;

    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }

    public void Die()
    {
        if (respawnPoint == null)
        {
            Debug.LogWarning("No respawn point assigned.");
            return;
        }

        if (rigidbodyPlayer != null)
        {
            rigidbodyPlayer.linearVelocity = Vector2.zero;
            rigidbodyPlayer.angularVelocity = 0f;
        }

        transform.position = respawnPoint.position;
    }
}