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
    private Transform currentTarget;
    private int cnt = 0;
    private bool returnedToPlayer = false; // 중복 전환 방지

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.stoppingDistance = FollowDistance;
        currentTarget = playerTarget;
    }

    void Update()
    {
        // cnt가 1이 되면 다시 플레이어를 타깃으로 전환
        if (cnt == 1 && !returnedToPlayer)
        {
            isFollowingPlayer = true;
            agent.SetDestination(playerTarget.position);
            returnedToPlayer = true; // 한 번만 전환
        }

        // 타겟 갱신
        currentTarget = isFollowingPlayer ? playerTarget : mazeTarget;

        // 목적지 설정은 타겟이 바뀌었거나, 경로가 없을 때만
        if (!agent.pathPending &&
            (agent.destination != currentTarget.position || !agent.hasPath))
        {
            agent.SetDestination(currentTarget.position);
        }

        // 남은 거리가 stoppingDistance 이내면 멈춤
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

    // 대화가 끝날 때 호출
    public void FollowMazeTarget()
    {
        isFollowingPlayer = false;
        cnt++;
        returnedToPlayer = false; // 다시 전환 허용
        if (mazeTarget != null)
        {
            agent.SetDestination(mazeTarget.position);
        }
    }

    // 충돌 처리
    private void OnTriggerEnter(Collider other)
    {
        if (!isFollowingPlayer && other.transform == mazeTarget)
        {
            animator.SetBool("isMoving", false);
            agent.ResetPath();
            // 추가 행동(예: 애니메이션, 이벤트 등)
        }
    }
}
