using UnityEngine.SceneManagement;
using OpSpark;
using UnityEngine;

public class IntroManager : TransitionManager
{
  private float timeRemaining = 5f;
  private bool transitioningAway = false;

  void Update()
  {
    if (timeRemaining > 0)
    {
      timeRemaining -= Time.deltaTime;
    }
    else if (!transitioningAway)
    {
      transitioningAway = true;
      panelFader.FadeOut(Strings.LOBBY);
    }
  }

  public void Lobby()
  {
    // executed by event trigger in TransitionManager
    SceneManager.LoadScene(Strings.LOBBY);
  }
}
