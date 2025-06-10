using System.Collections;
using UnityEngine;
using UnityEngine.UI; // �ݵ�� �߰�

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor = Color.black;
    public AnimationCurve fadeCurve;

    private Image image; // �Ǵ� public RawImage rawImage;
    private bool isFading = false;

    void Start()
    {
        image = GetComponent<Image>(); // Image ������Ʈ ��� ��
        // �Ǵ� rawImage = GetComponent<RawImage>(); // RawImage ������Ʈ ��� ��

        if (image != null) // �Ǵ� rawImage != null
            image.enabled = false; // �ʱ� ���� ����

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

        if (image != null) // �Ǵ� rawImage != null
        {
            image.enabled = true;
            image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alphaIn);
        }

        float timer = 0;
        while (timer <= fadeDuration)
        {
            float alpha = Mathf.Lerp(alphaIn, alphaOut, fadeCurve.Evaluate(timer / fadeDuration));
            if (image != null) // �Ǵ� rawImage != null
                image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

            timer += Time.deltaTime;
            yield return null;
        }

        if (image != null) // �Ǵ� rawImage != null
        {
            image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alphaOut);
            if (alphaOut == 0)
                image.enabled = false; // ���̵�ƿ� �Ϸ� �� ����
        }

        isFading = false;
    }
}
