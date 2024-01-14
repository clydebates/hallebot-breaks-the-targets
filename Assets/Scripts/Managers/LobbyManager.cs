using UnityEngine.SceneManagement;
using OpSpark;

public class LobbyManager : TransitionManager
{

  public void OnCharacterSelect()
  {
    panelFader.FadeOut(Strings.GAME);
  }

  public void Game()
  {
    // executed by event trigger in TransitionManager
    SceneManager.LoadScene(Strings.GAME);
  }
}
