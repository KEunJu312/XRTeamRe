//using OVR.OpenVR;
//using UnityEngine;
//using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
//public class AITarget : MonoBehaviour
//{
//    public float FollowDistance = 10.0f;
//    public Transform target;

//    private NavMeshAgent agent;
//    private Animator animator;
//    private float distance;



//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        animator = GetComponent<Animator>();


//    }

//    void Update()
//    {
//        distance = Vector3.Distance(agent.transform.position, target.position);

//        if (distance > FollowDistance)
//        {
//            animator.SetBool("isMoving", true);

//            // agent.destination = target.position;
//            Vector3 direction = (agent.transform.position - target.position).normalized;
//            Vector3 desiredPosition = target.position + direction * FollowDistance;

//            agent.SetDestination(desiredPosition);
//        }
//        else
//        {
//            animator.SetBool("isMoving", false);
//            agent.ResetPath();
//        }
//    }
}
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
        Debug.Log("현재 타겟: " + currentTarget.name);
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
        Debug.Log("FollowMazeTarget 호출됨");

    }

    // 충돌 처리
    private void OnTriggerEnter(Collider other)
    {
        if (!isFollowingPlayer && other.transform == mazeTarget)
        {
            // 미로 도착 시 원하는 행동 실행
            animator.SetBool("isMoving", false);
            agent.ResetPath();
            // 추가 행동(예: 애니메이션, 이벤트 등)
        }
    }
}
