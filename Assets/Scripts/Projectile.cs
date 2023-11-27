using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D col)
    {
        // TODO: add some particle effects
        // one for hitting a target, one for hitting something else
        Destroy(this.gameObject);
    }
}