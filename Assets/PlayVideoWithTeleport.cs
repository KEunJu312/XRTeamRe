using UnityEngine;
using UnityEngine.Video;

public class PlayVideoWithTeleport : MonoBehaviour
{
    public VideoPlayer videoPlayer;        // ���� ����� VideoPlayer
    public Transform player;               // �̵���ų �÷��̾� ������Ʈ (XR Rig ��)
    public Transform teleportTarget;       // �̵��� ��ġ (�� GameObject ��)

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            Debug.Log("�÷��̾ ���� ����!"); // �α� Ȯ��
            hasPlayed = true;

            // 1. �÷��̾� ��ġ �̵� (�ڷ���Ʈ)
            if (player != null && teleportTarget != null)
            {
                // XR Rig�� Camera Offset(�Ǵ� XR Origin) ��ġ �̵�
                player.position = teleportTarget.position;
                player.rotation = teleportTarget.rotation; // ���⵵ ����

                // ���� CharacterController�� �ִٸ�
                CharacterController cc = player.GetComponent<CharacterController>();
                if (cc != null)
                {
                    cc.enabled = false;
                    player.position = teleportTarget.position;
                    player.rotation = teleportTarget.rotation;
                    cc.enabled = true;
                }
            }

            // 2. ���� ���
            if (videoPlayer != null && !videoPlayer.isPlaying)
            {
                videoPlayer.Play();
            }
        }
    }
}
