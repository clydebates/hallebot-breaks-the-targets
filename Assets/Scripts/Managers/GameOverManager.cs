using System.Collections;
using OpSpark;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : TransitionManager
{
  [SerializeField] GameObject mainCamera;
  [SerializeField] float musicFadeOutSpeed;

  public void OnClickRestart()
  {
    StartCoroutine(FadeOut(mainCamera.GetComponent<AudioSource>(), musicFadeOutSpeed));
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

  private IEnumerator FadeOut(AudioSource audioSource, float fadeSpeed)
  {
    float startVolume = audioSource.volume;
    while (audioSource.volume > 0)
    {
      audioSource.volume -= startVolume * Time.deltaTime / fadeSpeed;
      yield return null;
    }

    audioSource.Stop();
    audioSource.volume = startVolume;
  }
}
