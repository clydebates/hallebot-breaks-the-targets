using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using OpSpark;

public class FireController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] [Range(5, 20)] float xVelocity = 20f;
    [SerializeField] float lifespan = 2f;

    [Header("Required Game Objects")]
    [SerializeField] GameObject fireSpawnPoint;
    [SerializeField] GameObject prefabFireBall;

    bool allow = true;

    // COMPONENTS //
    HorizontalController hc;
    Animator animator;

    
    private void Awake()
    {
        hc = GetComponent<HorizontalController>();
        animator = GetComponent<Animator>();
    }

    public void OnFireAway()
    {
        // instatiate prefab //
        GameObject projectile = Instantiate(prefabFireBall, fireSpawnPoint.transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = fireSpawnPoint.transform.right * xVelocity * transform.localScale.x;
        }

        // kill the projectile after its lifespan expires //
        Destroy(projectile, lifespan);
    }

    public void OnAnimationEnd()
    {
        hc.enabled = true;
        allow = true;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed && allow)
        {
            allow = false;
            hc.enabled = false;
            animator.SetTrigger(Strings.FIRE);
        }
    }
}
