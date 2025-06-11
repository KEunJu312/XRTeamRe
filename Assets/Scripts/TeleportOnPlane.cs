using UnityEngine;

public class TeleportOnPlane : MonoBehaviour
{
    public Transform spawnPoint;           // �̵��� ��ġ
    public float checkRadius = 0.3f;       // �浹 ���� ����
    public LayerMask teleportLayer;        // Plane�� �ִ� ���̾�

    void Update()
    {
        // �ֺ� �浹 ����
        Collider[] hits = Physics.OverlapSphere(transform.position, checkRadius, teleportLayer);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Teleport")) 
            {
                transform.position = spawnPoint.position;
                break;
            }
        }
    }
}

