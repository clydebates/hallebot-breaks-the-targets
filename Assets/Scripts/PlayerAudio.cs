using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
  [Header("SFX Tracks")]
  [SerializeField] AudioClip laserSound;
  [SerializeField] AudioClip jetpackTakeoff;

  private AudioSource audioSource;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    audioSource.loop = false;
  }

  public void PlayLaserSound()
  {
    audioSource.clip = laserSound;
    PlayLaserSound(1f, 1f);
  }

  public void PlayLaserSound(float volume, float pitch)
  {
    if (laserSound != null)
    {
      audioSource.clip = laserSound;
      audioSource.volume = volume;
      audioSource.pitch = pitch;
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
    if (audioSource.isPlaying && audioSource.clip == jetpackTakeoff)
    {
      audioSource.Stop();
    }
  }

  private void PlayIfNotAlready()
  {
    if (!audioSource.isPlaying) audioSource.Play();
  }
}
