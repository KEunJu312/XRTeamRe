using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class DoorVideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage videoScreen; // Canvas�� RawImage

    void Start()
    {
        if (videoScreen != null)
        {
            videoScreen.enabled = false; // ó���� ����
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (videoPlayer != null && videoScreen != null)
            {
                videoScreen.enabled = true; // ���� UI Ȱ��ȭ
                videoPlayer.Play();
            }
        }
    }
}
