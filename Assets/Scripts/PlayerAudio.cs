using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
  [Header("SFX Tracks")]
  [SerializeField] AudioClip laserSound;
  [SerializeField] AudioClip jetpackTakeoff;
  readonly float laserVolume = 0.5f;

  private AudioSource audioSource;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    audioSource.loop = false;
  }

  public void PlayLaserSound()
  {
    if (laserSound != null)
    {
      audioSource.clip = laserSound;
      audioSource.volume = laserVolume;
      audioSource.Play();
    }
  }

  public void PlayJumpSound()
  {
    if (jetpackTakeoff != null)
    {
      audioSource.clip = jetpackTakeoff;
      PlayIfNotAlready();
    }
  }

  public void CancelJumpSound()
  {
    if (audioSource.isPlaying)
    {
      audioSource.Stop();
    }
  }

  private void PlayIfNotAlready()
  {
    if (!audioSource.isPlaying) audioSource.Play();
  }
}
