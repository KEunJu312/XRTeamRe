using UnityEngine;
using UnityEngine.AI;

public class TeleportOnCollision : MonoBehaviour
{

    public Vector3 teleportPosition = new Vector3(351f, 5f, 468f);

    void OnTriggerEnter(Collider other)
    {
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
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

