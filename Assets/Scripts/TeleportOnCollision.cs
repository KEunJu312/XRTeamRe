using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    // �ڷ���Ʈ ��ġ�� ������ ���� (���ϴ� ��ġ�� ����)
    public Vector3 teleportPosition = new Vector3(351f, 5f, 468f);

    // ������Ʈ�� ť���� Ʈ���ſ� ������ �ڵ����� ����Ǵ� �Լ�
    void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"�� ��쿡�� ����
        if (other.CompareTag("Player") || other.CompareTag("Rabbit"))
        {
            // �÷��̾ ������ ��ġ�� �̵�
            other.transform.position = teleportPosition;
        }
    }
}
