using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonController : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject.CompareTag("Player"))
    {
      GameManager.Instance.Death();
    }
  }
}
