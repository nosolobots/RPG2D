using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    // This class handles the UI fade in and fade out effects.
    // It uses a singleton pattern to ensure only one instance exists.
    // The fade effect is applied to a UI Image component that covers the screen.

    // The fade image should be set in the inspector.
    // The fade duration can also be set in the inspector.

    [SerializeField] Image fadeImage;
    [SerializeField] float fadeDuration;

    public float FadeDuration
    {
        get { return fadeDuration; }
    }

    private IEnumerator fadeCoroutine;

    public void FadeToBlack()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = Fade(1f);
        StartCoroutine(fadeCoroutine);
    }

    public void FadeFromBlack()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = Fade(0f);
        StartCoroutine(fadeCoroutine);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        Color color = fadeImage.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
