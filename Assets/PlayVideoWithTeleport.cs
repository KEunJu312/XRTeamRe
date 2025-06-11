using UnityEngine;
using UnityEngine.Video;

public class PlayVideoWithTeleport : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Transform player;
    public Transform teleportTarget;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            Debug.Log("플레이어가 문에 닿음!");
            hasPlayed = true;

            // 1. 플레이어 위치 이동 (텔레포트)
            if (player != null && teleportTarget != null)
            {
                player.position = teleportTarget.position;
                player.rotation = teleportTarget.rotation;

                CharacterController cc = player.GetComponent<CharacterController>();
                if (cc != null)
                {
                    cc.enabled = false;
                    player.position = teleportTarget.position;
                    player.rotation = teleportTarget.rotation;
                    cc.enabled = true;
                }
            }

            // 2. 영상 재생
            if (videoPlayer != null && !videoPlayer.isPlaying)
            {
                videoPlayer.Play();
            }
        }
    }
}
