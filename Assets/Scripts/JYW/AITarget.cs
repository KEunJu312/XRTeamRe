using UnityEngine;
using UnityEngine.AI;

public class AITarget : MonoBehaviour
{
    public Transform playerTarget; // 플레이어
    public Transform mazeTarget;   // teleport to maze 오브젝트
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

    // 대화가 끝날 때 호출
    public void FollowMazeTarget()
    {
        isFollowingPlayer = false;
    }

    // 충돌 처리
    private void OnTriggerEnter(Collider other)
    {
        if (!isFollowingPlayer && other.transform == mazeTarget)
        {
            // 미로 도착 시 원하는 행동 실행
            animator.SetBool("isMoving", false);
            agent.ResetPath();

            // 타깃을 다시 플레이어로 변경
            isFollowingPlayer = true;
        }
    }
}
