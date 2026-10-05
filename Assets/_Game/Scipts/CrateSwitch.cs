using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CrateSwitch : MonoBehaviour
{
    //[Header("Values")]

    [Header("Setup")]
    [SerializeField] private string tagBeingSearchedFor = "crate";

    [Header("Debug")]
    [Header("Please do not change anything. This area is only for checking values.")]
    [SerializeField] private BoxCollider2D colliderAreaOfEffect;



    void Awake()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>())
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("CrateSwitch:OnTriggerEnter");
        if (collision.gameObject.tag == tagBeingSearchedFor)
        {
            Debug.Log("CrateSwitch: OnTriggerEnter: other.gameObject.tag(" + collision.gameObject.tag + ") == tagBeingSearchedFor(" + tagBeingSearchedFor + ")");
            collision.gameObject.GetComponent<Crate>().ToggelCreate();
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3();

        if (Application.isEditor && !colliderAreaOfEffect)
        {
            colliderAreaOfEffect = this.gameObject.GetComponent<BoxCollider2D>();
        }

        size.x = colliderAreaOfEffect.size.x;
        size.y = colliderAreaOfEffect.size.y;

        Gizmos.matrix = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        Gizmos.color = new Color(0f, 0f, 1f, 0.25f);
        Gizmos.DrawCube(Vector3.zero, size);
        Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
