using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private bool ForceToRight;
    [SerializeField] private float ForceValue = 1f;

    [Header("Setup")]


    [Header("Debug")]
    [Header("Please do not change anything. This area is only for checking values.")]
    [SerializeField] private BoxCollider2D colliderAreaOfEffect;

    private void Awake()
    {
        if (!colliderAreaOfEffect)
        {
            colliderAreaOfEffect = this.gameObject.GetComponent<BoxCollider2D>();
        }
        colliderAreaOfEffect.isTrigger = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector3 force = new Vector3();

            force += this.gameObject.transform.right * ForceValue;

            if (!ForceToRight)
            {
                force *= -1f;
            }

            collision.GetComponent<PlayerMovment>().ReciveSlideInput(force);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3();

        if (Application.isEditor && !colliderAreaOfEffect)
        {
            colliderAreaOfEffect = this.gameObject.GetComponent<BoxCollider2D>();
        }

        size.x = colliderAreaOfEffect.size.x /** this.gameObject.transform.localScale.x*/;
        size.y = colliderAreaOfEffect.size.y /** this.gameObject.transform.localScale.y*/;

        //Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        // Set the color with custom alpha.
        Gizmos.color = new Color(0f, 0f, 1f, 0.25f);
        // Draw the cube.
        //Gizmos.DrawCube(this.transform.position, size);
        Gizmos.DrawCube(Vector3.zero, size);

        // Draw a wire cube outline.
        Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
        //Gizmos.DrawWireCube(this.transform.position, size);
        Gizmos.DrawWireCube(Vector3.zero, size);

        Gizmos.color = Color.yellow;
        Vector3 ForceDirectionArrowFlagpole = Vector3.up * 0.5f;
        Vector3 ForceDirectionArrowTip = new Vector3();
        ForceDirectionArrowTip = Vector3.right * 0.5f;
        if (!ForceToRight)
        {
            ForceDirectionArrowTip *= -1;
        }

        Gizmos.DrawLine(-ForceDirectionArrowFlagpole, ForceDirectionArrowFlagpole);
        Gizmos.DrawLine(ForceDirectionArrowFlagpole * 0.75f, Vector3.zero + ForceDirectionArrowTip);
        Gizmos.DrawLine(ForceDirectionArrowFlagpole * -0.75f, Vector3.zero + ForceDirectionArrowTip);
    }
}
