using UnityEngine;
using DoorScript; // ���ӽ����̽� ���� �߰�

public class KeyDestruction : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            // Door ������Ʈ ��������
            Door door = other.GetComponent<Door>();
            if (door != null)
            {
                door.OpenDoor(); // �� ���� �޼��� ȣ��
            }
            Destroy(gameObject); // Ű ����
        }
    }
}

