using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor = Color.black;
    public AnimationCurve fadeCurve;

    private Image image;
    private bool isFading = false;

    void Start()
    {
        image = GetComponent<Image>();
    
        if (image != null) 
            image.enabled = false;

        if (fadeOnStart)
            FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        if (!isFading)
            StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        isFading = true;

        if (image != null)
        {
            image.enabled = true;
            image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alphaIn);
        }

        float timer = 0;
        while (timer <= fadeDuration)
        {
            float alpha = Mathf.Lerp(alphaIn, alphaOut, fadeCurve.Evaluate(timer / fadeDuration));
            if (image != null)
                image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        if (image != null)
        {
            image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alphaOut);
            if (alphaOut == 0)
                image.enabled = false;
        }

        isFading = false;
    }
}
