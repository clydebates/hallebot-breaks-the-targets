using System.Collections;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
  [Header("Background Tracks")]
  public AudioClip BgIntro;
  public AudioClip BgLoop;

  private AudioSource[] audioSources;
  private AudioSource audioSource1;
  private AudioSource audioSource2;

  // Start is called before the first frame update
  void Start()
  {
    if (BgIntro == null && BgLoop == null) return;

    audioSources = GetComponents<AudioSource>();
    for (int i = 0; i< audioSources.Length; i++)
    {
      audioSources[i].loop = false;
      audioSources[i].volume = 1;
      audioSources[i].pitch = 1f;
    }
    audioSource1 = audioSources[0];
    if (audioSources.Length > 1) audioSource2 = audioSources[1]; 
    PlayBgLoop();
  }

  private void PlayBgLoop()
  {
    if (BgIntro == null)
    {
      audioSource1.clip = BgLoop;
      audioSource1.loop = true;
      audioSource1.Play();
      return;
    }

    audioSource1.clip = BgIntro;
    audioSource1.loop = false;
    audioSource2.clip = BgLoop;
    audioSource2.loop = true;
    audioSource1.Play();
    audioSource2.PlayDelayed(audioSource1.clip.length);
  }

  public void MusicFadeOut()
  {
    if (audioSource1 == null) return;

    if (audioSource1.isPlaying) StartCoroutine(FadeOut(audioSource1, 2.5f));
    if (audioSource2 != null && audioSource2.isPlaying) StartCoroutine(FadeOut(audioSource2, 2.5f));
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
