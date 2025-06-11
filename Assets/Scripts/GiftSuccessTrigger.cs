using UnityEngine;

public class GiftSuccessTrigger : MonoBehaviour
{
    public AudioClip collectSound; // 효과음
    public AudioSource audioSource; //

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("성공 상자입니다.");
            PlaySound();
            Destroy(gameObject, collectSound.length); // 소리 길이만큼 딜레이 후 삭제
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

