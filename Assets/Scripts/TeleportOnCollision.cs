using UnityEngine;
using UnityEngine.AI;

public class TeleportOnCollision : MonoBehaviour
{
    // 텔레포트 위치를 지정할 변수 (원하는 위치를 설정)
    public Vector3 teleportPosition = new Vector3(351f, 5f, 468f);

    void OnTriggerEnter(Collider other)
    {
        // NavMeshAgent가 붙어있는 오브젝트(예: Rabbit, Player)만 처리
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            // NavMesh 위에서만 Warp 가능
            bool success = agent.Warp(teleportPosition);
            if (!success)
            {
                Debug.LogWarning($"{other.name}의 Warp 실패: 지정 위치가 NavMesh 위에 없습니다.");
            }
        }
        else
        {
            // NavMeshAgent 없는 경우엔 Transform.position 사용 (예외적 상황)
            other.transform.position = teleportPosition;
        }
    }
}

