using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class DoorVideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage videoScreen; // Canvas의 RawImage

    void Start()
    {
        if (videoScreen != null)
        {
            videoScreen.enabled = false; // 처음엔 숨김
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (videoPlayer != null && videoScreen != null)
            {
                videoScreen.enabled = true; // 영상 UI 활성화
                videoPlayer.Play();
            }
        }
    }
}
