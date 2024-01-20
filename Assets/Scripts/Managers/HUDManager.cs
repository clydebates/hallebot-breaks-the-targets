using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
  public TextMeshProUGUI text;

  // Update is called once per frame
  void Update()
  {
    text.text = GameManager.Instance.GameScore.ToString();      
  }
}
