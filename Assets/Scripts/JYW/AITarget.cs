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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Transform currentTarget = isFollowingPlayer ? playerTarget : mazeTarget;
        float distance = Vector3.Distance(agent.transform.position, currentTarget.position);

        if (distance > FollowDistance)
        {
            animator.SetBool("isMoving", true);
            agent.SetDestination(currentTarget.position);
        }
        else
        {
            animator.SetBool("isMoving", false);
            agent.ResetPath();
        }
    }

    // ��ȭ�� ���� �� ȣ��
    public void FollowMazeTarget()
    {
        isFollowingPlayer = false;
    }

    // �浹 ó��
    private void OnTriggerEnter(Collider other)
    {
        if (!isFollowingPlayer && other.transform == mazeTarget)
        {
            // �̷� ���� �� ���ϴ� �ൿ ����
            animator.SetBool("isMoving", false);
            agent.ResetPath();

            // Ÿ���� �ٽ� �÷��̾�� ����
            isFollowingPlayer = true;
        }
    }
}
