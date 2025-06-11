using UnityEngine;

public class TeleportOnPlane : MonoBehaviour
{
    public Transform spawnPoint;           // 이동할 위치
    public float checkRadius = 0.3f;       // 충돌 감지 범위
    public LayerMask teleportLayer;        // Plane이 있는 레이어

    void Update()
    {
        // 주변 충돌 감지
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

