using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FlashbackTriggerWithHeartMiniGame : MonoBehaviour
{
    public Volume postProcessingVolume;
    public GameObject heartBeatMiniGameUI;

    private bool isFlashingBack = false;
    private bool isEscapeConditionMet = false;
    private bool hasTriggered = false;

    private AudioSource heartbeatAudioSource; // ������ҽ� ���� ����

    void Start()
    {
        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(false);

        // AudioPlayer ������Ʈ���� AudioSource ��������
        GameObject audioPlayer = GameObject.Find("AudioPlayer");
        if (audioPlayer != null)
        {
            heartbeatAudioSource = audioPlayer.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("AudioPlayer ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFlashingBack && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(FlashbackSequence());
        }
    }

    private IEnumerator FlashbackSequence()
    {
        isFlashingBack = true;
        isEscapeConditionMet = false;

        Debug.Log("Flashback ����");

        // ����� ���
        if (heartbeatAudioSource != null && heartbeatAudioSource.clip != null)
        {
            heartbeatAudioSource.Play();
        }

        if (postProcessingVolume != null)
            postProcessingVolume.weight = 1f;

        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(true);

        while (!isEscapeConditionMet)
        {
            yield return null;
        }

        // ����� ����
        if (heartbeatAudioSource != null && heartbeatAudioSource.isPlaying)
        {
            heartbeatAudioSource.Stop();
        }

        if (postProcessingVolume != null)
            postProcessingVolume.weight = 0f;

        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(false);

        Debug.Log("Flashback Ż�� ����!");

        isFlashingBack = false;
        Destroy(gameObject);
    }

    public void SetEscapeConditionMet()
    {
        isEscapeConditionMet = true;
    }
}
