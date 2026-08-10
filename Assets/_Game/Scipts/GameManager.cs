using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


//contains temporary code for simple checkpoints to reposition the player
// ToDo: Change Switch to a better checkpoint system or delete it later.
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject myPlayer;
    [SerializeField]
    private Vector2 checkPointPosition;

    [Tooltip("Please do not change anything. This area is only for checking values.")]
    [Header("Debug")]
    public static GameManager StaticInstanceGameManager;

    private void Awake()
    {
        if (StaticInstanceGameManager == null)
        {
            StaticInstanceGameManager = this;
        }
        else
        {
            Debug.LogError("The singleton instance for the GameManager has already been defined. " +
                            "Therefore, this object is considered redundant and is being deleted.");
            Object.Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Temporary code to reset to the starting point
        myPlayer = GameObject.FindAnyObjectByType<PlayerMovment>().gameObject;
        checkPointPosition = myPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetPlayer()
    {
        myPlayer.transform.position = checkPointPosition;
    }
}
