using UnityEngine;

public class XRTriggerTeleport : MonoBehaviour
{
    public Transform spawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleportTartget"))  // Plane ������Ʈ�� �±�
        {
            // XR Origin�� ��ġ�� Spawn���� �̵�
            transform.position = spawnPoint.position;
        }
    }
}

