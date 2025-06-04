using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FlashbackTrigger : MonoBehaviour
{
    public Volume postProcessingVolume;  // Post Processing Volume (Global or Local)
    public float flashbackDuration = 3f;  // 플래시백 지속 시간

    private bool isFlashingBack = false;

    private void OnTriggerEnter(Collider other)
    {
        // XR Origin (플레이어) 와 충돌 시
        if (other.CompareTag("Player") && !isFlashingBack)
        {
            StartCoroutine(FlashbackSequence());
        }
    }

    private IEnumerator FlashbackSequence()
    {
        isFlashingBack = true;
        Debug.Log("Flashback 시작");

        // Post Processing 효과 활성화 (예: Chromatic Aberration, Vignette 등)
        postProcessingVolume.weight = 1f;

        // 사운드 효과나 카메라 흔들림 등 추가 가능

        yield return new WaitForSeconds(flashbackDuration);

        // 효과 복귀
        postProcessingVolume.weight = 0f;
        Debug.Log("Flashback 종료");

        isFlashingBack = false;
    }
}
