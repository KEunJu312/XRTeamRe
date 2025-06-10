using UnityEngine;
using UnityEngine.AI;

public class AITarget : MonoBehaviour
{
    public Transform playerTarget; // �÷��̾�
    public Transform mazeTarget;   // teleport to maze ������Ʈ
    public float FollowDistance = 1.0f;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isFollowingPlayer = true;
    private Transform currentTarget;
    private int cnt = 0;
    private bool returnedToPlayer = false; // �ߺ� ��ȯ ����

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.stoppingDistance = FollowDistance;
        currentTarget = playerTarget;
    }

    void Update()
    {
        // cnt�� 1�� �Ǹ� �ٽ� �÷��̾ Ÿ������ ��ȯ
        if (cnt == 1 && !returnedToPlayer)
        {
            isFollowingPlayer = true;
            agent.SetDestination(playerTarget.position);
            returnedToPlayer = true; // �� ���� ��ȯ
        }

        // Ÿ�� ����
        currentTarget = isFollowingPlayer ? playerTarget : mazeTarget;

        // ������ ������ Ÿ���� �ٲ���ų�, ��ΰ� ���� ����
        if (!agent.pathPending &&
            (agent.destination != currentTarget.position || !agent.hasPath))
        {
            agent.SetDestination(currentTarget.position);
        }

        // ���� �Ÿ��� stoppingDistance �̳��� ����
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            animator.SetBool("isMoving", false);
            agent.ResetPath();
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }

    // ��ȭ�� ���� �� ȣ��
    public void FollowMazeTarget()
    {
        isFollowingPlayer = false;
        cnt++;
        returnedToPlayer = false; // �ٽ� ��ȯ ���
        if (mazeTarget != null)
        {
            agent.SetDestination(mazeTarget.position);
        }
    }

    // �浹 ó��
    private void OnTriggerEnter(Collider other)
    {
        if (!isFollowingPlayer && other.transform == mazeTarget)
        {
            animator.SetBool("isMoving", false);
            agent.ResetPath();
            // �߰� �ൿ(��: �ִϸ��̼�, �̺�Ʈ ��)
        }
    }
}
