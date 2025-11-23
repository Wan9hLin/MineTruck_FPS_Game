using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    public CanvasGroup bossWarning;
    public float fadeDuration = 0.6f;

    private void Start()
    {
        bossWarning = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        bossWarning.alpha = Mathf.Lerp(0, 1, Mathf.PingPong(Time.time, fadeDuration));
    }
}
