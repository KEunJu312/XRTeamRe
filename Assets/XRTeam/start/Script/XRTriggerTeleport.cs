using UnityEngine;

public class XRTriggerTeleport : MonoBehaviour
{
    public Transform spawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleportTartget"))  // Plane 오브젝트의 태그
        {
            // XR Origin의 위치를 Spawn으로 이동
            transform.position = spawnPoint.position;
        }
    }
}

