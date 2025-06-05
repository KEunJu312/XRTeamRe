using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerCamera;

    void Start()
    {
        // XR ȯ�濡�� Main Camera�� �ڵ����� ������
        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;
    }

    void Update()
    {
        if (playerCamera != null)
        {
            // ��ȭâ�� �׻� �÷��̾ �ٶ󺸰�
            Vector3 lookPos = playerCamera.position;
            lookPos.y = transform.position.y; // �ʿ��ϸ� y�� ȸ���� ����
            transform.LookAt(lookPos);

            // ���� ĵ���� �ո��� �ݴ�� ���̸� �Ʒ� �ڵ�� ��ü
            // transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.position);
        }
    }
}
