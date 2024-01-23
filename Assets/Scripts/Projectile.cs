using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
  void OnCollisionEnter2D(Collision2D col)
  {
    Destroy(this.gameObject);
  }
}