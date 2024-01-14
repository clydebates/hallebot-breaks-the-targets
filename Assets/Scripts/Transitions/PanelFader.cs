using UnityEngine;

public class PanelFader : MonoBehaviour
{
  private string callbackName;
  public OpSpark.AnimationEvent EventAnimationComplete;

  public void FadeOut(string callbackName)
  {
    this.callbackName = callbackName;
    GetComponent<Animator>().SetTrigger("fadeOut");
  }

  public void OnFadeOutComplete()
  {
    EventAnimationComplete.Invoke(callbackName);
  }
}
