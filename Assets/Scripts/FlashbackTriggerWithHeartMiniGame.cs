using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FlashbackTriggerWithHeartMiniGame : MonoBehaviour
{
    public Volume postProcessingVolume;
    public GameObject heartBeatMiniGameUI;

    private bool isFlashingBack = false;
    private bool isEscapeConditionMet = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFlashingBack)
        {
            StartCoroutine(FlashbackSequence());
        }
    }

    private IEnumerator FlashbackSequence()
    {
        isFlashingBack = true;
        Debug.Log("Flashback 시작");

        postProcessingVolume.weight = 1f;
        heartBeatMiniGameUI.SetActive(true);

        while (!isEscapeConditionMet)
        {
            yield return null;
        }

        postProcessingVolume.weight = 0f;
        heartBeatMiniGameUI.SetActive(false);
        Debug.Log("Flashback 탈출 성공!");

        isFlashingBack = false;
    }

    public void SetEscapeConditionMet()
    {
        isEscapeConditionMet = true;
    }
}
