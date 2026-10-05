using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Checkpoint touched by: {other.gameObject.name}");

        PlayerDeath playerDeath =
            other.GetComponentInParent<PlayerDeath>();

        if (playerDeath == null)
        {
            Debug.LogWarning(
                $"No PlayerDeath found on {other.gameObject.name} or its parents."
            );

            return;
        }

        playerDeath.SetRespawnPoint(transform);

        Debug.Log($"Checkpoint activated: {gameObject.name}");
    }
}