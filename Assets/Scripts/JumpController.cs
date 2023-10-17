using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using OpSpark;

public class JumpController : MonoBehaviour
{
    [SerializeField] float jumpForce = 15;
    [SerializeField] float maxJumpDuration = 0.8f;
    [SerializeField] [Range(1, 10)] float antiGravity = 7;
    [SerializeField] [Range(1, 10)] float rateOfAcceleration = 5;
    [SerializeField] [Range(0.1f, 0.7f)] float snapBackRate = 0.3f;

    bool isPressed = false;
    int jumpCount = 0;
    int jumpEndedCount = 0;
    Vector2 defaultGravity;
    
    float duration = 0;

    // COMPONENTS //
    Rigidbody2D rb;
    Animator animator;
    BoxCollider2D feet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        defaultGravity = new Vector2(0f, -Physics2D.gravity.y);
    }

    /*
     * BONUS TODO: Improve: Could you AddForce(), instead? Tweak all parameters...
     */
    private void FixedUpdate()
    {
        // TODO: update y velocity on what conditions? //
        if(IsTouchingPlatform() && isPressed)
        {
            PerformJump();
            jumpCount++;
        }

        // TODO: update rate of return to platform //
        if(rb.velocity.y < 0)
        {
            // we're falling //
            rb.velocity -= defaultGravity * rateOfAcceleration * Time.deltaTime;
            if (isPressed && jumpCount < 2 && jumpEndedCount > 0)
            {
                // double jump
                PerformJump();
                jumpCount++;
            }
        }


        // TODO: jump higher while key pressed //
        if(isPressed && rb.velocity.y > 0)
        {
            duration += Time.deltaTime;
            if (duration > maxJumpDuration) isPressed = false;

            float pointInJumpDuration = duration / maxJumpDuration;
            float appliedAntiGravity = antiGravity;

            // if we're more than halfway through the allowable jump duration //
            if(pointInJumpDuration > 0.5f)
            {
                appliedAntiGravity = antiGravity * (1 - pointInJumpDuration);
            }

            rb.velocity += defaultGravity * appliedAntiGravity * Time.deltaTime;
        }

        // player releases button //
        if(!isPressed)
        {
            duration = 0;
            animator.SetBool(Strings.IS_JUMPING, false);
            // but we're still falling, so snap back to earth a bit //
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * snapBackRate);
            }
        }
    }

    private void PerformJump() 
    {
        duration = 0;
        animator.SetBool(Strings.IS_JUMPING, true);
        float toY = jumpForce;
        rb.velocity = new Vector2(rb.velocity.x, toY);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // intent to jump //
            isPressed = true;
        }
        else if (context.performed)
        {
            // jump is happening //
            isPressed = true;
        }
        else if (context.canceled)
        {
            // jump should end //
            isPressed = false;
            jumpEndedCount++;
        }
    }

    private bool IsTouchingPlatform()
    {
        if (feet.IsTouchingLayers(LayerMask.GetMask("Platform"))) 
        {
            jumpEndedCount = 0;
            jumpCount = 0;
            return true;
        }
        return false;
    }
}
