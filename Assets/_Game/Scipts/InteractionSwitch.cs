using UnityEngine;

// ToDo: implementierung des neue Unity Inputsystems
// ToDo: Aktionstaste anzeigen
// ToDo: Grafik switchen
// ToDo: Objekt zum referenzieren was geschaltet werden soll
// ToDo: 

[RequireComponent(typeof(BoxCollider2D))]
public class InteractionSwitch : MonoBehaviour
{

    //[Header("Values")]

    [Header("Setup")]
    [SerializeField] private GameObject graphicForActionButton;
    [SerializeField] private GameObject graphicForSwitchOff;
    [SerializeField] private GameObject graphicForSwitchOn;
    [SerializeField] private Dor_switched scriptOffInteractionSwitch;
    [SerializeField] private string tagBeingSearchedFor = "Player";

    [Header("Debug")]
    [Header("Please do not change anything. This area is only for checking values.")]
    [SerializeField] private BoxCollider2D colliderAreaOfEffect;
    [SerializeField] private bool PlayerInCollider;
    [SerializeField] private bool InteractionState = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckSwitchGraphic();
    }

    // ToDo: Ensure better input detection
    void Update()
    {

        if (PlayerInCollider && Input.GetKeyDown(KeyCode.E))
        {
            TriggerInteraction();
        }
    }

    private void TriggerInteraction()
    {
        scriptOffInteractionSwitch.ReciveInteractionTrigger();
        CheckSwitchGraphic();
    }

    private void CheckSwitchGraphic()
    {
        if (!graphicForSwitchOff || !graphicForSwitchOn)
        {
            return;
        }
        if (InteractionState)
        {
            graphicForSwitchOn.SetActive(true);
            graphicForSwitchOff.SetActive(false);
        }
        else
        {
            graphicForSwitchOff.SetActive(true);
            graphicForSwitchOn.SetActive(false);
        }
    }

    //ToDo: use a better system to detect the player 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInCollider = true;
        }
    }

    ////ToDo: use a better system to detect the player 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInCollider = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isEditor && !colliderAreaOfEffect)
        {
            colliderAreaOfEffect = this.gameObject.GetComponent<BoxCollider2D>();
            colliderAreaOfEffect.isTrigger = true;
        }
        Gizmos.color = new Color(1f, 0.75f, 0.5f, 0.25f);
        Gizmos.DrawCube((Vector2)this.transform.position + colliderAreaOfEffect.offset, colliderAreaOfEffect.size);

    }
}
