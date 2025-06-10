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

    private AudioSource heartbeatAudioSource;

    void Start()
    {
        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(false);

        GameObject audioPlayer = GameObject.Find("AudioPlayer");
        if (audioPlayer != null)
        {
            heartbeatAudioSource = audioPlayer.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("AudioPlayer 오브젝트를 찾을 수 없습니다.");
        }

        // 시작 시 post-processing 비활성화
        if (postProcessingVolume != null)
        {
            postProcessingVolume.weight = 0f;
            postProcessingVolume.enabled = false;
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

        Debug.Log("Flashback 시작");

        // 오디오 재생
        if (heartbeatAudioSource != null && heartbeatAudioSource.clip != null)
        {
            heartbeatAudioSource.Play();
        }

        // 후처리 효과 적용
        if (postProcessingVolume != null)
        {
            postProcessingVolume.enabled = true;
            postProcessingVolume.weight = 1f;
        }

        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(true);

        while (!isEscapeConditionMet)
        {
            yield return null;
        }

        // 오디오 정지
        if (heartbeatAudioSource != null && heartbeatAudioSource.isPlaying)
        {
            heartbeatAudioSource.Stop();
        }

        // 후처리 효과 해제
        if (postProcessingVolume != null)
        {
            postProcessingVolume.weight = 0f;
            postProcessingVolume.enabled = false;
        }

        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(false);

        Debug.Log("Flashback 탈출 성공!");

        isFlashingBack = false;
        Destroy(gameObject);
    }

    public void SetEscapeConditionMet()
    {
        isEscapeConditionMet = true;
    }
}
