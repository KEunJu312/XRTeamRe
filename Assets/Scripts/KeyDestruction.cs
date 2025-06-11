using UnityEngine;
using UnityEngine.Video;

public class KeyDestruction : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ���̾��Ű�� VideoPlayer ������Ʈ�� ���⿡ �Ҵ�

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            // ���� ���
            if (videoPlayer != null)
            {
                videoPlayer.Play();
            }
            Destroy(gameObject); // Ű ����
        }
    }
}


