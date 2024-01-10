using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  // TODO: Character select
  // Enable chosen character Game Object on/before main scene load
  // Set correct transform to the Follow property on the FollowCamera
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
