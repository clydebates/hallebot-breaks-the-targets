using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;

    void OnEnable()
    {
      GameManager.OnPlayerDeath += EnableGameOverMenu;
    }

    void OnDisable()
    {
      GameManager.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
      gameOverMenu.SetActive(true);
    }
}
