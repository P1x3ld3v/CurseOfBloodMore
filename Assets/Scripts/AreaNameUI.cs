using System.Collections;
using TMPro;
using UnityEngine;

public class AreaNameUI : MonoBehaviour
{
    public static AreaNameUI Instance;

    [SerializeField] private TMP_Text text;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Timing")]
    [SerializeField] private float fadeInTime = 2.8f;
    [SerializeField] private float visibleTime = 3.5f;
    [SerializeField] private float fadeOutTime = 2.8f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        Instance = this;
        canvasGroup.alpha = 0f;
    }

    public void Show(string areaName)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(FadeRoutine(areaName));
    }

    private IEnumerator FadeRoutine(string areaName)
    {
        text.text = areaName;

        yield return Fade(0f, 1f, fadeInTime);
        yield return new WaitForSecondsRealtime(visibleTime); 
        yield return Fade(1f, 0f, fadeOutTime);

        canvasGroup.alpha = 0f;
        text.text = "";
    }

    private IEnumerator Fade(float start, float end, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime; 
            float t = time / duration;

            t = Mathf.SmoothStep(0f, 1f, t);

            canvasGroup.alpha = Mathf.Lerp(start, end, t);
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}