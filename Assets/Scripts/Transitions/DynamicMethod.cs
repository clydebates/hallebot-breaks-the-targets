using System.Reflection;
using UnityEngine;

public abstract class DynamicMethod : MonoBehaviour
{
  public void Execute(string callbackName)
  {
    MethodInfo methodInfo = GetType().GetMethod(callbackName);
    methodInfo?.Invoke(this, null);
  }
}
