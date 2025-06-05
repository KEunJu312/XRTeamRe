using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FlashbackTriggerWithHeartMiniGame : MonoBehaviour
{
    public Volume postProcessingVolume;
    public GameObject heartBeatMiniGameUI;

    private bool isFlashingBack = false;
    private bool isEscapeConditionMet = false;
    private bool hasTriggered = false; // 트리거가 한 번만 작동하도록

    void Start()
    {
        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFlashingBack && !hasTriggered)
        {
            hasTriggered = true; // 트리거가 한 번만 작동
            StartCoroutine(FlashbackSequence());
        }
    }

    private IEnumerator FlashbackSequence()
    {
        isFlashingBack = true;
        isEscapeConditionMet = false;

        Debug.Log("Flashback 시작");

        if (postProcessingVolume != null)
            postProcessingVolume.weight = 1f;

        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(true);

        while (!isEscapeConditionMet)
        {
            yield return null;
        }

        if (postProcessingVolume != null)
            postProcessingVolume.weight = 0f;

        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(false);

        Debug.Log("Flashback 탈출 성공!");

        isFlashingBack = false;
        Destroy(gameObject); // 미니게임 종료 후 트리거 오브젝트 삭제
    }

    public void SetEscapeConditionMet()
    {
        isEscapeConditionMet = true;
    }
}
