using System.Collections;
using System.Collections.Generic;
using OpSpark;
using UnityEngine;
using UnityEngine.InputSystem;

public class HorizontalController : MonoBehaviour
{
    [SerializeField] float rateOfMovement = 10;
    [SerializeField] [Range(1, 10)] float rateOfAcceleration;

    float acceleration;
    float direction = 1f;
    bool isPressed = false;

    Vector2 inputMovement;

    // COMPONENTS //
    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.localScale = new Vector2(direction, 1f);
        if(acceleration == 0)
        {
            animator.SetBool(Strings.IS_RUNNING, false);
        }
        else
        {
            animator.SetBool(Strings.IS_RUNNING, true);
        }
    }

    /*
     * BONUS TODO: Improve: Could you AddForce(), instead? Tweak all parameters...
     */
    private void FixedUpdate()
    {
        UpdateAcceleration();
        float toX = rateOfMovement * acceleration * direction;
        rb.velocity = new Vector2(toX, rb.velocity.y);
    }

    /*
     * TODO: Improve: Avoid up/down arrows triggering horizontal movement!
     */
    private void UpdateAcceleration()
    {
        if(isPressed && acceleration < 1)
        {
            acceleration += Time.deltaTime * rateOfAcceleration;
        }
        else if(!isPressed && acceleration != 0)
        {
            acceleration -= Time.deltaTime * rateOfAcceleration;
            if (acceleration < 0) acceleration = 0;
        }
    }

    /*
     * The `context` is an event-type object with data about the input-action!
     */
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // intent to move //
            isPressed = true;
        }
        else if(context.performed)
        {
            // movement is happening //
            isPressed = true;
        }
        else if (context.canceled)
        {
            // movement should end //
            isPressed = false;
        }
        //Debug.Log($"isPressed : {isPressed}");

        /*
         * The Move action produces a Vector2 (x, y), registering x and y axis input.
         */ 
        inputMovement = context.ReadValue<Vector2>();
        //Debug.Log($"inputMovement : {inputMovement}");
        switch (inputMovement.x)
        {
            case > 0:
                direction = 1;
                break;
            case < 0:
                direction = -1;
                break;
        }
    }
}
