using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action OnPlayerDeath;

    void Awake()
    {
      Instance = this;
    }

    public void GameOver()
    {
      OnPlayerDeath?.Invoke();
    }

    public void RestartGame()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
