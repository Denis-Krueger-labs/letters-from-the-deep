using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovment : MonoBehaviour
{

    [Header("Movement Values")]
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float jumpingPower = 100f;
    [SerializeField] Vector2 gravity = new Vector2(0f, 2.45f);


    [Header("Setup")]
    [SerializeField]
    private Rigidbody2D rigidbodyPlayer;
    [SerializeField]
    private CircleCollider2D colliderforGoundCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlatform;
    [SerializeField]
    private GameObject holderForDirectionFlip;



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
    }

    //Physics-based checks are used to determine whether the player is on the ground or on a platform.
    private void FixedUpdate()
    {
        distanceOfGoundCheck = colliderforGoundCheck.radius;
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
        //velocity = Vector2.zero;

        float localeVariableFuerInput = 0f;
        localeVariableFuerInput = Input.GetAxis("Horizontal");

        

        if (localeVariableFuerInput > 0f)
        {
            velocity.x = horizontalSpeed * Time.deltaTime;
        }
        else if (localeVariableFuerInput < 0f)
        {
            velocity.x = -horizontalSpeed * Time.deltaTime;
        }

        bool isJumping = false;
        isJumping = Input.GetButtonDown("Jump");

        //if (isJumping && isGounded)
        //{
        //    //velocity.y = jumpSpeed;
        //    //rigidbodyPlayer.AddForceY(jumpSpeed);
        //    //AddForceToPlayer(jumpingPower);
        //}

        //if (isGounded)
        //{
        //    velocity.y = -0.02f * Time.deltaTime;
        //}
        //else
        //{
        //    velocity += gravity * Time.deltaTime * Time.deltaTime;
        //}
        if (isJumping)
        {
            //velocity.y += jumpingPower;
            AddForceToPlayer(jumpingPower * Time.deltaTime);
        }

        //velocity.y = velocity.y + Physics.gravity.y;
        //velocity.y += Physics.gravity.y * Time.deltaTime;

        //velocity *= Time.deltaTime;

        Physics2D.gravity = gravity;
        this.transform.Translate(velocity.x, velocity.y, 0f);

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

    public void AddForceToPlayer(float recivedBostUP)
    {
        rigidbodyPlayer.AddForceY(recivedBostUP, ForceMode2D.Impulse);
    }

    public void AddForceToPlayer(Vector2 recivedForce)
    {
        rigidbodyPlayer.AddForce(recivedForce);
    }

    //TODo: change to maby OnDrawGizmosSelected
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(colliderforGoundCheck.transform.position, distanceOfGoundCheck);
    }
}
