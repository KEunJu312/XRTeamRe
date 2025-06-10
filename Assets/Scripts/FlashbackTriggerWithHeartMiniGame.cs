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

    private AudioSource heartbeatAudioSource; // 오디오소스 참조 변수

    void Start()
    {
        if (heartBeatMiniGameUI != null)
            heartBeatMiniGameUI.SetActive(false);

        // AudioPlayer 오브젝트에서 AudioSource 가져오기
        GameObject audioPlayer = GameObject.Find("AudioPlayer");
        if (audioPlayer != null)
        {
            heartbeatAudioSource = audioPlayer.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("AudioPlayer 오브젝트를 찾을 수 없습니다.");
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

        if (postProcessingVolume != null)
            postProcessingVolume.weight = 1f;

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

        if (postProcessingVolume != null)
            postProcessingVolume.weight = 0f;

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
