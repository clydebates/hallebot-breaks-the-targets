using OpSpark;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : TransitionManager
{
  public void OnClickPlayAgain()
  {
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
