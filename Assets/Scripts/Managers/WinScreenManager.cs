using System;
using OpSpark;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : TransitionManager
{
  [SerializeField] TextMeshProUGUI timeValueText;
  [SerializeField] GameObject mainCamera;
  BackgroundAudio bgAudio;

  void Awake()
  {
    bgAudio = mainCamera.GetComponent<BackgroundAudio>();
    if (GameManager.Instance)
    {
      TimeSpan time = TimeSpan.FromSeconds(GameManager.Instance.TotalGameTime);
      //here backslash is must to tell that colon is
      //not the part of format, it just a character that we want in output
      timeValueText.text = time.ToString(@"mm\:ss");
    }
  }

  public void OnClickPlayAgain()
  {
    bgAudio.MusicFadeOut();
    panelFader.FadeOut(Strings.LOBBY);
  }

  public void OnClickQuit()
  {
    Application.Quit();
  }

  public void Lobby()
  {
    // executed by event trigger in TransitionManager
    SceneManager.LoadScene(Strings.LOBBY);
  }
}
