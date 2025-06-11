using UnityEngine;
using UnityEngine.Video;

public class KeyDestruction : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 하이어라키의 VideoPlayer 오브젝트를 여기에 할당

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


