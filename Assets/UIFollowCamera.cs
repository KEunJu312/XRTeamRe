using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Transform playerCamera; // XR Origin의 Main Camera를 할당
    public float distanceFromCamera = 2.0f; // 카메라로부터의 거리

    void Start()
    {
        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;
    }

    void Update()
    {
        if (playerCamera == null) return;

        // 카메라 앞, 원하는 거리 위치 계산
        Vector3 targetPosition = playerCamera.position + playerCamera.forward * distanceFromCamera;
        transform.position = targetPosition;

        // 항상 카메라 바라보게 회전
        transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.position);
    }
}
