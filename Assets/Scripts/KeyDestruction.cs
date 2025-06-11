using UnityEngine;
using UnityEngine.Video;

public class KeyDestruction : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            // 영상 재생
            if (videoPlayer != null)
            {
                videoPlayer.Play();
            }
            Destroy(gameObject); // 키 삭제
        }
    }
}


