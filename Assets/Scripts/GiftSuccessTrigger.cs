using UnityEngine;

public class GiftSuccessTrigger : MonoBehaviour
{
    public AudioClip collectSound; // ����� ȿ����
    public AudioSource audioSource; // �ν����Ϳ��� Audio Source�� �巡�� �� ������� �Ҵ�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("���� �����Դϴ�.");
            PlaySound();
            Destroy(gameObject, collectSound.length); // �Ҹ� ���̸�ŭ ������ �� ����
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

