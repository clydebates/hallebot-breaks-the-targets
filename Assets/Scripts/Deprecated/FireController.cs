using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using OpSpark;

public class FireController : Projectile
{
    [Header("Params")]
    [SerializeField] [Range(5f, 20f)] float xVelocity = 20f;
    [SerializeField] float lifespan = 2f;

    [Header("Required Game Objects")]
    [SerializeField] GameObject fireSpawnPoint;
    [SerializeField] GameObject prefabFireball; 
    Animator animator;
    HorizontalController hc;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        hc = GetComponent<HorizontalController>();
    }

    public void OnAnimationEnd() {
        hc.enabled = true;
    }

    //fired from the Animator   
    public void OnFireAway() {
        // instantiate prefab
        GameObject projectile = Instantiate(
            prefabFireball, 
            fireSpawnPoint.transform.position, 
            Quaternion.identity);
        
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = fireSpawnPoint.transform.right * xVelocity * transform.localScale.x;

        // destroy object after certain amount of time
        Destroy(projectile, lifespan);
    }

    public void OnFire(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            animator.SetTrigger(Strings.FIRE);
        }
    }
}
