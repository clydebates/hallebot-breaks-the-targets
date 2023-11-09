using System.Collections;
using UnityEngine;
using OpSpark;

public class TargetController : MonoBehaviour
{
    // TODO: this should be in a GameManager
    int scoreCount = 0;
    Animator animator;

    [SerializeField] [Range(0.1f, 0.9f)] float explosionLifespan = 0.4f;

    void Awake()
    {
      animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Projectile"))
        {
            // explode target
            animator.SetTrigger(Strings.EXPLODE);
            // add to score count
            scoreCount++;
            // destroy projectile
            Destroy(collider.gameObject);
            // destroy target
            Destroy(this.gameObject, explosionLifespan);
        }
    }
}
