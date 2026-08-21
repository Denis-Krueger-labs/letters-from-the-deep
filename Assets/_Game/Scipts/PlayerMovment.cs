using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    [Header("Movement Values")]
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float jumpingPower = 100f;
    [SerializeField] Vector2 gravity = new Vector2(0f, 2.45f);


    [Header("Setup")]
    [SerializeField] private Rigidbody2D rigidbodyPlayer;
    [SerializeField] private CircleCollider2D colliderforGoundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlatform;
    [SerializeField] private GameObject holderForDirectionFlip;



    [Header("Debug")]
    [Header("Please do not change anything. This area is only for checking values.")]
    [SerializeField] private Vector2 velocity = Vector2.zero;
    [SerializeField] private bool isGounded;
    [SerializeField] private bool isOnPlatform;
    [SerializeField] private GameObject platform;
    [SerializeField] private float distanceOfGoundCheck = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rigidbodyPlayer == null)
        {
            rigidbodyPlayer = this.gameObject.GetComponent<Rigidbody2D>();
        }
        distanceOfGoundCheck = colliderforGoundCheck.radius;
    }

    /// <summary>
    /// Physics-based checks are used to determine whether the player is on the ground or on a platform.
    /// </summary>
    private void FixedUpdate()
    { 
        if (colliderforGoundCheck.IsTouchingLayers(whatIsGround))  // es wird abgefragt om in diesem Array elemente existieren
        {
            // wenn das der Fall ist dann ist unser Spieler am Boden
            isGounded = true;
        }
        else
        {
            // wenn das nicht unser Fall ist dann ist unser Spieler nicht am Boden
            isGounded = false;
        }

        PlatformCheck();
    }

    // Update is called once per frame
    void Update()
    {
        CalculationOfMovementHorizontal();
        CalculationOfMovementVertical();
    }

    /// <summary>
    /// Checks whether the player is on a platform and sets the player as a child of the platform.
    /// Uses "this" for setting the reference 
    /// ToDo: Switch to the official player object, because "this" isn't always fitting.
    /// </summary>
    private void PlatformCheck()
    {
        if (colliderforGoundCheck.IsTouchingLayers(whatIsPlatform))
        {
            isOnPlatform = true;
            Collider2D platformCollider = Physics2D.OverlapCircle(colliderforGoundCheck.transform.position, distanceOfGoundCheck, whatIsPlatform);
            // ToDo: Switch to the official player object, because "this" isn't always fitting.
            this.transform.SetParent(platformCollider.transform);
        }
        else
        {
            isOnPlatform = false;
            // ToDo: Switch to the official player object, because "this" isn't always fitting.
            this.transform.SetParent(null);
        }

    }

    /// <summary>
    /// The horizontal input (for example, "A" and "D") is multiplied by "horizontalSpeed".
    /// Together with "Time.deltaTime", this object is moved using "transform.Translate".
    /// <para> ToDo: Integrating Unity's new input system </para>
    /// <para> ToDo: Switch to the official player object, because "this" isn't always fitting. </para>
    /// </summary>
    private void CalculationOfMovementHorizontal()
    {
        float localeVariableFuerInput = 0f;
        localeVariableFuerInput = Input.GetAxis("Horizontal");

        if (localeVariableFuerInput != 0f)
        {
            velocity.x = horizontalSpeed * localeVariableFuerInput;
        }
        else
        {
            velocity.x = 0f;
        }

        this.transform.Translate(velocity.x * Time.deltaTime, 0f, 0f);
    }

    /// <summary>
    ///         A value is constantly calculated to move the player downwards: 
    /// <para>  when the player is on the ground, -0.01f 
    /// <br>    limited to a maximum of -0.02f to prevent excessive forces  </br> </para>  
    /// <para>  and gravity.y * Time.deltaTime when they are not on the ground  </para>
    /// <para>  When the jump input(e.g., the spacebar) is triggered, "jumpingPower" is also factored into the calculation.     </para>
    /// <para>  Together with "Time.deltaTime", this object is moved using "transform.Translate".   </para>
    /// <para>  ToDo: Integrating Unity's new input system  </para>
    /// <para>  ToDo: Switch to the official player object, because "this" isn't always fitting.    </para>
    /// </summary>
    private void CalculationOfMovementVertical()
    {

        bool isJumping = false;
        isJumping = Input.GetButtonDown("Jump");

        if (isGounded)
        {
            velocity.y += -0.01f;
            velocity.y = Mathf.Clamp(velocity.y, -0.02f, float.MaxValue);
        }
        else
        {
            velocity.y += gravity.y * Time.deltaTime;
        }
        if (isJumping)
        {
            velocity.y += jumpingPower;
        }

        this.transform.Translate(0f, velocity.y * Time.deltaTime, 0f);
    }

    /// <summary>
    /// Currently used to visualize the ground check area.
    /// maby ToDo: change to OnDrawGizmosSelected
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(colliderforGoundCheck.transform.position, distanceOfGoundCheck);
    }
}
