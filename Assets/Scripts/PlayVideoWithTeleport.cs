using UnityEngine;
using UnityEngine.Video;

public class PlayVideoWithTeleport : MonoBehaviour
{
    public VideoPlayer videoPlayer;        // 영상 재생용 VideoPlayer
    public Transform player;               // 이동시킬 플레이어 오브젝트 (XR Rig 등)
    public Transform teleportTarget;       // 이동할 위치 (빈 GameObject 등)

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            Debug.Log("플레이어가 문에 닿음!"); // 로그 확인
            hasPlayed = true;

            // 1. 플레이어 위치 이동 (텔레포트)
            if (player != null && teleportTarget != null)
            {
                // XR Rig의 Camera Offset(또는 XR Origin) 위치 이동
                player.position = teleportTarget.position;
                player.rotation = teleportTarget.rotation; // 방향도 맞춤

                // 만약 CharacterController가 있다면
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
