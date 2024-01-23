using System;
using Cinemachine;
using OpSpark;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public static event Action OnPlayerDeath;
  private int gameScore;
  private BackgroundAudio backgroundAudio;

  [SerializeField] GameObject hallebot;
  [SerializeField] GameObject spaceman;
  [SerializeField] GameObject mainCamera;
  [SerializeField] CinemachineVirtualCamera followCamera;

  public int GameScore { get => gameScore; }

  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }

    gameScore = GameObject.FindGameObjectsWithTag(Strings.TARGET).Length;
    backgroundAudio = mainCamera.GetComponent<BackgroundAudio>();
    SetCharacter(LobbyManager.Instance.CharacterSelection);
  }

  public void GameOver()
  {
    backgroundAudio.MusicFadeOut(0.5f);
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
    }
  }

  public int DecrementGameScore()
  {
    --gameScore;
    if (gameScore == 0) YouWin();
    return gameScore;
  }

  private void YouWin()
  {
    backgroundAudio.MusicFadeOut(0.5f);
    SceneManager.LoadScene(Strings.WIN);
  }
}
