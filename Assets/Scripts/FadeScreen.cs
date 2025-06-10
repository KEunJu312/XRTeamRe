using System.Collections;
using UnityEngine;
using UnityEngine.UI; // 반드시 추가

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor = Color.black;
    public AnimationCurve fadeCurve;

    private Image image; // 또는 public RawImage rawImage;
    private bool isFading = false;

    void Start()
    {
        image = GetComponent<Image>(); // Image 컴포넌트 사용 시
        // 또는 rawImage = GetComponent<RawImage>(); // RawImage 컴포넌트 사용 시

        if (image != null) // 또는 rawImage != null
            image.enabled = false; // 초기 상태 숨김

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

        if (image != null) // 또는 rawImage != null
        {
            image.enabled = true;
            image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alphaIn);
        }

        float timer = 0;
        while (timer <= fadeDuration)
        {
            float alpha = Mathf.Lerp(alphaIn, alphaOut, fadeCurve.Evaluate(timer / fadeDuration));
            if (image != null) // 또는 rawImage != null
                image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        if (image != null) // 또는 rawImage != null
        {
            image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alphaOut);
            if (alphaOut == 0)
                image.enabled = false; // 페이드아웃 완료 시 숨김
        }

        isFading = false;
    }
}
