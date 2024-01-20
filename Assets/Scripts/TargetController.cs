using UnityEngine;
using OpSpark;

public class TargetController : MonoBehaviour
{
    Animator animator;

    [SerializeField] [Range(0.1f, 0.9f)] float explosionLifespan = 0.4f;

    void Awake()
    {
      animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
  {
      if (collision.gameObject.CompareTag("Projectile"))
      {
        // explode target
        animator.SetTrigger(Strings.EXPLODE);
        // destroy projectile
        Destroy(collision.gameObject);
        // decrement game score
        GameManager.Instance.DecrementGameScore();
        // destroy target
        Destroy(this.gameObject, explosionLifespan);
      }
    }
}
