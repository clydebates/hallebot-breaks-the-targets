using UnityEngine;

public class TransitionManager : DynamicMethod
{
  [SerializeField] protected PanelFader panelFader;
  // Start is called before the first frame update
  void Start()
  {
    panelFader.EventAnimationComplete.AddListener(OnFadeOutComplete);
  }

  public void OnFadeOutComplete(string callbackName)
  {
    panelFader.EventAnimationComplete.RemoveListener(OnFadeOutComplete);
    Execute(callbackName);
  }
}
