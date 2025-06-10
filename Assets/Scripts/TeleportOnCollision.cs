using UnityEngine;
using UnityEngine.AI;

public class TeleportOnCollision : MonoBehaviour
{
    // �ڷ���Ʈ ��ġ�� ������ ���� (���ϴ� ��ġ�� ����)
    public Vector3 teleportPosition = new Vector3(351f, 5f, 468f);

    void OnTriggerEnter(Collider other)
    {
        // NavMeshAgent�� �پ��ִ� ������Ʈ(��: Rabbit, Player)�� ó��
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            // NavMesh �������� Warp ����
            bool success = agent.Warp(teleportPosition);
            if (!success)
            {
                Debug.LogWarning($"{other.name}�� Warp ����: ���� ��ġ�� NavMesh ���� �����ϴ�.");
            }
        }
        else
        {
            // NavMeshAgent ���� ��쿣 Transform.position ��� (������ ��Ȳ)
            other.transform.position = teleportPosition;
        }
    }
}

