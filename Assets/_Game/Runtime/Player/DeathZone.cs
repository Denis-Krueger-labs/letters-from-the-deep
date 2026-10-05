using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private enum DeathMode
    {
        Instant,
        Timed
    }

    [Header("Death Settings")]
    [SerializeField] private DeathMode deathMode = DeathMode.Instant;

    [Min(0.1f)]
    [SerializeField] private float timeUntilDeath = 5f;

    [Header("Debug")]
    [SerializeField] private float currentTimer = 0f;

    private readonly HashSet<Collider2D> playerColliders = new();

    private PlayerDeath currentPlayer;
    private bool playerKilled;

    private void Update()
    {
        if (deathMode != DeathMode.Timed)
        {
            return;
        }

        if (currentPlayer == null || playerColliders.Count == 0)
        {
            return;
        }

        if (playerKilled)
        {
            return;
        }

        currentTimer += Time.deltaTime;

        if (currentTimer >= timeUntilDeath)
        {
            KillPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDeath playerDeath =
            other.GetComponentInParent<PlayerDeath>();

        if (playerDeath == null)
        {
            return;
        }

        playerColliders.Add(other);

        currentPlayer = playerDeath;

        if (deathMode == DeathMode.Instant && !playerKilled)
        {
            KillPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!playerColliders.Remove(other))
        {
            return;
        }

        if (playerColliders.Count == 0)
        {
            ResetDeathTimer();
        }
    }

    private void KillPlayer()
    {
        if (currentPlayer == null)
        {
            return;
        }

        playerKilled = true;

        currentPlayer.Die();
    }

    private void ResetDeathTimer()
    {
        currentTimer = 0f;
        currentPlayer = null;
        playerKilled = false;
    }
}