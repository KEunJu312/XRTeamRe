using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerCamera;

    void Start()
    {
        if (playerCamera == null && Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (playerCamera != null)
        {
            // 캔버스의 앞면이 플레이어를 바라보게 회전
            Vector3 direction = (transform.position - playerCamera.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            // Vector3 targetPosition = playerCamera.position;
            // targetPosition.y = transform.position.y;
            // Vector3 direction = (transform.position - targetPosition).normalized;
            // transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}


