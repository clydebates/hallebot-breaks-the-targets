using Cinemachine;
using OpSpark;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : TransitionManager
{
  public static GameManager Instance;
  [HideInInspector] public bool IsPlayerDead = false;
  [HideInInspector] public float TotalGameTime;

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

  public void Death()
  {
    IsPlayerDead = true;
    backgroundAudio.MusicFadeOut(0.5f);
    panelFader.FadeOut(Strings.GAME_OVER);
  }

  public void GameOver()
  {
    SceneManager.LoadScene(Strings.GAME_OVER);
  }

  public void RestartGame()
  {
    SceneManager.LoadScene(Strings.LOBBY);
  }

  public void OnClickQuit()
  {
    Application.Quit();
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
    TotalGameTime = Time.timeSinceLevelLoad;
    backgroundAudio.MusicFadeOut(0.5f);
    panelFader.FadeOut(Strings.WIN);
  }

  public void Win()
  {
    SceneManager.LoadScene(Strings.WIN);
  }
}
