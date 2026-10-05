using UnityEngine;

public class Player_CrateDetection : MonoBehaviour
{
    [Header("Values")]


    [Header("Setup")]
    [SerializeField] private BoxCollider2D colliderForCrateCheck;
    [SerializeField] private LayerMask whatIsCrate;


    [Header("Debug")]
    [Header("Please do not change anything. This area is only for checking values.")]
    [SerializeField] private float distanceOfGoundCheck = 0.05f;
    [SerializeField] private bool isNearCrate;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlatformCheck();
    }
    private void PlatformCheck()
    {
        if (colliderForCrateCheck.IsTouchingLayers(whatIsCrate))
        {
            isNearCrate = true;
        }
        else
        {
            isNearCrate = false;
        }

    }

    public bool GetNearCrateState()
    {
        return isNearCrate;
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3();
        Vector3 colliderOffset = new Vector3();

        if (Application.isEditor && !colliderForCrateCheck)
        {
            Debug.LogError("Player_CrateDetection: Please assign the collider.");
            //colliderForCrateCheck = this.gameObject.GetComponent<BoxCollider2D>();
        }

        size.x = colliderForCrateCheck.size.x;
        size.y = colliderForCrateCheck.size.y;
        colliderOffset.x = colliderForCrateCheck.offset.x;
        colliderOffset.y = colliderForCrateCheck.offset.y;


        Gizmos.matrix = Matrix4x4.TRS(colliderForCrateCheck.gameObject.transform.position+ colliderOffset, 
                                        transform.localRotation, 
                                        transform.localScale);
        Gizmos.color = new Color(0f, 0f, 1f, 0.25f);
        Gizmos.DrawCube(Vector3.zero, size);
        Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
