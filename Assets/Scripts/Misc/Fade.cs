using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] float fadeDelay;
    
    [Range(0, 1)]
    [SerializeField] float fadeAlpha;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeTransition(fadeAlpha));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeTransition(1));
        }
    }
    
    IEnumerator FadeTransition(float alpha)
    {
        float fadeTime = 0;
        float currentAlpha = sr.color.a;

        while (fadeTime < fadeDelay)
        {
            fadeTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(currentAlpha, alpha, fadeTime / fadeDelay);
            Color color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
            sr.color = color;
            yield return null;
        }
    }
}
