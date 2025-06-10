using UnityEngine;

public class GiftFailTrigger : MonoBehaviour
{
    public AudioClip collectSound; // ����� ȿ����

    private AudioSource audioSource;

    void Start()
    {
        // AudioSource �ڵ� �߰� �Ǵ� ����
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
            Debug.Log("���� �����Դϴ�.");
            PlaySound();
            Destroy(gameObject); // ���� ������Ʈ ����
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
