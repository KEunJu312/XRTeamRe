using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Transform playerCamera; // XR Origin�� Main Camera�� �Ҵ�
    public float distanceFromCamera = 2.0f; // ī�޶�κ����� �Ÿ�

    void Start()
    {
        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;
    }

    void Update()
    {
        if (playerCamera == null) return;

        // ī�޶� ��, ���ϴ� �Ÿ� ��ġ ���
        Vector3 targetPosition = playerCamera.position + playerCamera.forward * distanceFromCamera;
        transform.position = targetPosition;

        // �׻� ī�޶� �ٶ󺸰� ȸ��
        transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.position);
    }
}
