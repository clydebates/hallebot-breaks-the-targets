using UnityEngine.SceneManagement;
using OpSpark;

public class LobbyManager : TransitionManager
{
  public static LobbyManager Instance;
  private int characterSelection = 0;

  public int CharacterSelection { get => characterSelection; }

  void Awake()
  {
    if (!Instance)
    {
      Instance = this;
    }
  }

  public void OnCharacterSelect(int character)
  {
    characterSelection = character;
    panelFader.FadeOut(Strings.GAME);
  }

  public void Game()
  {
    // executed by event trigger in TransitionManager
    SceneManager.LoadScene(Strings.GAME);
  }
}
