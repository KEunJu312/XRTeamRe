using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    // 텔레포트 위치를 지정할 변수 (원하는 위치를 설정)
    public Vector3 teleportPosition = new Vector3(51f, 0f, 168f);

    // 오브젝트가 큐브의 트리거에 닿으면 자동으로 실행되는 함수
    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 "Player"인 경우에만 실행
        if (other.CompareTag("Player"))
        {
            // 플레이어를 지정된 위치로 이동
            other.transform.position = teleportPosition;
        }
    }
}
