using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // 플레이어 카메라(헤드셋) Transform을 Inspector에서 할당하거나 자동으로 찾음
    public Transform playerCamera;

    void Start()
    {
        // playerCamera가 비어 있으면 Main Camera를 자동으로 할당
        if (playerCamera == null && Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (playerCamera != null)
        {
            // 캔버스의 앞면(+Z)이 플레이어(카메라)를 바라보게 회전
            // (뒤집힘 현상 방지)
            Vector3 direction = (transform.position - playerCamera.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            // 만약 Y축만 회전시키고 싶으면 아래 코드로 교체:
            // Vector3 targetPosition = playerCamera.position;
            // targetPosition.y = transform.position.y;
            // Vector3 direction = (transform.position - targetPosition).normalized;
            // transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}

