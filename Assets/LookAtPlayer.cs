using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // �÷��̾� ī�޶�(����) Transform�� Inspector���� �Ҵ��ϰų� �ڵ����� ã��
    public Transform playerCamera;

    void Start()
    {
        // playerCamera�� ��� ������ Main Camera�� �ڵ����� �Ҵ�
        if (playerCamera == null && Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (playerCamera != null)
        {
            // ĵ������ �ո�(+Z)�� �÷��̾�(ī�޶�)�� �ٶ󺸰� ȸ��
            // (������ ���� ����)
            Vector3 direction = (transform.position - playerCamera.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            // ���� Y�ุ ȸ����Ű�� ������ �Ʒ� �ڵ�� ��ü:
            // Vector3 targetPosition = playerCamera.position;
            // targetPosition.y = transform.position.y;
            // Vector3 direction = (transform.position - targetPosition).normalized;
            // transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}

