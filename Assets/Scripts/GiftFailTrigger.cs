using UnityEngine;

public class GiftFailTrigger : MonoBehaviour
{
    public AudioClip collectSound; // 재생할 효과음

    private AudioSource audioSource;

    void Start()
    {
        // AudioSource 자동 추가 또는 참조
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("실패 상자입니다.");
            PlaySound();
            Destroy(gameObject); // 선물 오브젝트 제거
        }
    }

    void PlaySound()
    {
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }
}
