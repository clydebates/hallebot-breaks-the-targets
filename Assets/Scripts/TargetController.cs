using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    //this should be in a GameManager
    int scoreCount = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Projectile hit Target");
            // explode target
            // add to score count
            scoreCount++;
        }
    }
}
