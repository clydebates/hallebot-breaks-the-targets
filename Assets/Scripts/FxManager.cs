using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FxManager : MonoBehaviour
{
    public static FxManager Instance;
    public ParticleSystem fxDust;

    void Awake()
    {
      Instance = this;
    }

    public void KickupDust()
    {
      ParticleSystem dust = Instantiate(fxDust, GetComponentInParent<Transform>());
      Destroy(dust, dust.main.duration);
    }
}