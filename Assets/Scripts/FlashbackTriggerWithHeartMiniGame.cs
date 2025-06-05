using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FlashbackTriggerWithHeartMiniGame : MonoBehaviour
{
    public Volume postProcessingVolume;
    public GameObject heartBeatMiniGameUI;

    private bool isFlashingBack = false;
    private bool isEscapeConditionMet = false;

    // �� ���� �� ��Ʈ �̴ϰ��� UI�� �ڵ����� ��Ȱ��ȭ
    void Start()
    {
        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(false);
    }

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
        isEscapeConditionMet = false; // �ݺ� ��� �ø� ���� �ʱ�ȭ

        Debug.Log("Flashback ����");

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

        Debug.Log("Flashback Ż�� ����!");

        isFlashingBack = false;
    }

    // �ܺο��� �� �Լ��� ȣ���ؼ� Ż�� ������ ������ų �� ����
    public void SetEscapeConditionMet()
    {
        isEscapeConditionMet = true;
    }
}
