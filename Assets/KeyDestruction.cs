using UnityEngine;
using DoorScript; // 네임스페이스 참조 추가

public class KeyDestruction : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            // Door 컴포넌트 가져오기
            Door door = other.GetComponent<Door>();
            if (door != null)
            {
                door.OpenDoor(); // 문 열기 메서드 호출
            }
            Destroy(gameObject); // 키 삭제
        }
    }
}

