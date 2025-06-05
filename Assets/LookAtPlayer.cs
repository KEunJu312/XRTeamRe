using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerCamera;

    void Start()
    {
        // XR 환경에서 Main Camera가 자동으로 잡히게
        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;
    }

    void Update()
    {
        if (playerCamera != null)
        {
            // 대화창이 항상 플레이어를 바라보게
            Vector3 lookPos = playerCamera.position;
            lookPos.y = transform.position.y; // 필요하면 y축 회전만 적용
            transform.LookAt(lookPos);

            // 만약 캔버스 앞면이 반대로 보이면 아래 코드로 교체
            // transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.position);
        }
    }
}
