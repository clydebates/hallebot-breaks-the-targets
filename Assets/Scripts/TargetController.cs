using UnityEngine;
using OpSpark;

public class TargetController : MonoBehaviour
{
  Animator animator;
  CircleCollider2D circleCollider2D;

  [SerializeField][Range(0.1f, 0.9f)] float explosionLifespan = 0.4f;

  void Awake()
  {
    animator = GetComponent<Animator>();
    circleCollider2D = GetComponent<CircleCollider2D>();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Projectile"))
    {
      circleCollider2D.enabled = false;
      // explode target
      animator.SetTrigger(Strings.EXPLODE);
      // decrement game score
      GameManager.Instance.DecrementGameScore();
      // destroy target
      Destroy(this.gameObject, explosionLifespan);
    }
  }
}
