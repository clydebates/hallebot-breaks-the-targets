using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  // TODO: Character select
  // Enable chosen character Game Object on/before main scene load
  // Set correct transform to the Follow property on the FollowCamera
  public static GameManager Instance;
  public static event Action OnPlayerDeath;

  [SerializeField] GameObject hallebot;
  [SerializeField] GameObject spaceman;
  [SerializeField] CinemachineVirtualCamera followCamera;

  void Awake()
  {
    Instance = this;
    SetCharacter(LobbyManager.Instance.CharacterSelection);
  }

  public void GameOver()
  {
    OnPlayerDeath?.Invoke();
  }

  public void RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void SetCharacter(int characterSelection)
  {
    switch (characterSelection)
    {
      case 0:
        // disable spaceman, enable hallebot
        spaceman.SetActive(false);
        hallebot.SetActive(true);
        followCamera.Follow = hallebot.transform;
        break;
      case 1:
        // disable hallebot, enable spaceman
        hallebot.SetActive(false);
        spaceman.SetActive(true);
        followCamera.Follow = spaceman.transform;
        break;
      default:
        spaceman.SetActive(false);
        hallebot.SetActive(true);
        followCamera.Follow = hallebot.transform;
        break;
    }
  }
}
