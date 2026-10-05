using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{

    [Header("Values")]


    [Header("Setup")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovment playerMovmentScript;
    [SerializeField] private Player_CrateDetection crateDetectorScript;
    

    [Header("Debug")]
    [Header("Please do not change anything. This area is only for checking values.")]
    [SerializeField] private Vector2 velocity;
    [SerializeField] private bool isOnNearCrate;
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isIdle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        



        if (animator)
        {
            
            CheckForNewCrateState(crateDetectorScript.GetNearCrateState());
            CheckForNewGoundedAndJumpingState(playerMovmentScript.GetIsGounded(),playerMovmentScript.GetIsJumping());
            velocity = playerMovmentScript.GetVelocity();
            SetSendVelocityToAnimator();
        }



    }

    private void CheckForNewCrateState(bool recivedCrateState)
    {
        if (isOnNearCrate == recivedCrateState)
        {
            return;
        }

        isOnNearCrate = recivedCrateState;
        animator.SetBool("AtCrate", recivedCrateState);

    }
    private void CheckForNewGoundedAndJumpingState(bool recivedGounded, bool recivedJumping)
    {
        Debug.LogFormat("PlayerAnimationManager CheckForNewGoundedAndJumpingState: isOnGround({0}) | recivedGounded({1}) || isJumping({2}) | recivedJumping({3})", 
                            isOnGround, recivedGounded, isJumping, recivedJumping);

        if (isOnGround != recivedGounded)
        {
            isOnGround = recivedGounded;
            animator.SetBool("Gounded", recivedGounded);
        }
        if (isJumping != recivedJumping)
        {
            isJumping = recivedJumping;
            animator.SetBool("Jumping", recivedJumping);
        }
    }
    private void SetSendVelocityToAnimator()
    {
        if (isOnGround && Mathf.Abs( velocity.x) <0.05f)
        {
           isIdle = true;
        }
        else
        {
            isIdle = false;
        }
        animator.SetBool("Idel", isIdle);
        animator.SetFloat("VeloX", Mathf.Abs(velocity.x));
        animator.SetFloat("VeloY", velocity.y);
    }
}
