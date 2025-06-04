using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    // �ڷ���Ʈ ��ġ�� ������ ���� (���ϴ� ��ġ�� ����)
    public Vector3 teleportPosition = new Vector3(51f, 0f, 168f);

    // ������Ʈ�� ť���� Ʈ���ſ� ������ �ڵ����� ����Ǵ� �Լ�
    void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"�� ��쿡�� ����
        if (other.CompareTag("Player"))
        {
            // �÷��̾ ������ ��ġ�� �̵�
            other.transform.position = teleportPosition;
        }
    }
}
