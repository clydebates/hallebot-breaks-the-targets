using UnityEngine;

public class FxManager : MonoBehaviour
{
  public static FxManager Instance;
  public ParticleSystem fxDust;
  public ParticleSystem jetpackSmoke;

  void Awake()
  {
    Instance = this;
  }

  public void KickupDust()
  {
    ParticleSystem dust = Instantiate(fxDust, GetComponentInParent<Transform>());
    Destroy(dust, dust.main.duration);
  }

  public void JetpackDead(float duration)
  {
    ParticleSystem smoke = Instantiate(jetpackSmoke, GetComponentInParent<Transform>());
    Destroy(smoke, duration);
  }
}