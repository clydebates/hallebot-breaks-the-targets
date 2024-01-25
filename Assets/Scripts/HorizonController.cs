using UnityEngine;

public class HorizonController : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject.CompareTag("Player") && GameManager.Instance && GameManager.Instance.GameScore > 0)
    {
      GameManager.Instance.Death();
    }
  }
}
