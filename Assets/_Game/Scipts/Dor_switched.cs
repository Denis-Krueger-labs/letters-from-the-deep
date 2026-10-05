using Unity.Mathematics;
using UnityEngine;

public class Dor_switched : MonoBehaviour
{

    [Header("Values")]
    [SerializeField] private float TimeToMoveMax = 5f;


    [Header("Setup")]
    [SerializeField] private GameObject ObjectToMove;
    [SerializeField] private GameObject StartPosition;
    [SerializeField] private GameObject EndPosition;

    [Header("Debug")]
    [Header("Please do not change anything. This area is only for checking values.")]

    [SerializeField] private float timeToMoveRemaining;
    [SerializeField] private float timeToMovePassed;
    [SerializeField] private float movmentPercent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectToMove.transform.position = StartPosition.transform.position;
        ObjectToMove.transform.rotation = StartPosition.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToMoveRemaining > 0f)
        {
            MoveTheObject();
        }
    }

    public void ReciveInteractionTrigger()
    {
        timeToMoveRemaining = TimeToMoveMax;
        timeToMovePassed = 0f;

    }

    private void MoveTheObject()
    {
        timeToMoveRemaining -= Time.deltaTime;
        timeToMovePassed += Time.deltaTime;
        movmentPercent = timeToMovePassed / TimeToMoveMax;

        ObjectToMove.transform.position = Vector3.Lerp(StartPosition.transform.position, EndPosition.transform.position, movmentPercent);
        ObjectToMove.transform.rotation =  Quaternion.Lerp(StartPosition.transform.rotation, EndPosition.transform.rotation, movmentPercent);
    }
}
