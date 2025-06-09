using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RabbitController : MonoBehaviour
{
    [Header("Follow Settings")]
    public float FollowDistance = 10.0f;
    public Transform target; // 플레이어 Transform

    [Header("Teleport Settings")]
    public GameObject teleportToMaze; // teleport to maze 오브젝트
    public float teleportTriggerDistance = 1.5f; // 텔레포트 트리거 거리

    [Header("State Control")]
    public bool shouldFollowPlayer = false; // 플레이어를 따라다닐지 여부
    public bool isMovingToTeleport = false; // 텔레포트 지점으로 이동 중인지

    private NavMeshAgent agent;
    private Animator animator;
    private float distance;
    private bool hasTeleported = false;

    // 기존 AITarget과 동일한 구조
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // target 자동 찾기 (설정되지 않은 경우)
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
        }

        // teleport to maze 오브젝트 자동 찾기 (설정되지 않은 경우)
        if (teleportToMaze == null)
        {
            teleportToMaze = GameObject.Find("teleport to maze");
        }
    }

    void Update()
    {
        if (target == null) return;

        if (isMovingToTeleport && !hasTeleported)
        {
            // 텔레포트 지점으로 이동
            MoveToTeleportPoint();
        }
        else if (shouldFollowPlayer && hasTeleported)
        {
            // 기존 AITarget 로직으로 플레이어 따라다니기
            FollowPlayerLogic();
        }
        else if (shouldFollowPlayer && !isMovingToTeleport)
        {
            // 대화 전 기본 상태에서도 플레이어 따라다니기 (필요한 경우)
            FollowPlayerLogic();
        }
    }

    // 기존 AITarget과 동일한 플레이어 따라다니기 로직
    private void FollowPlayerLogic()
    {
        distance = Vector3.Distance(agent.transform.position, target.position);

        if (distance > FollowDistance)
        {
            if (animator != null)
                animator.SetBool("isMoving", true);

            // 기존 AITarget과 동일한 방식으로 목표 지점 계산
            Vector3 direction = (agent.transform.position - target.position).normalized;
            Vector3 desiredPosition = target.position + direction * FollowDistance;
            agent.SetDestination(desiredPosition);
        }
        else
        {
            if (animator != null)
                animator.SetBool("isMoving", false);
            agent.ResetPath();
        }
    }

    // 텔레포트 지점으로 이동하는 로직
    private void MoveToTeleportPoint()
    {
        if (teleportToMaze != null && agent != null)
        {
            // 애니메이션 상태 업데이트
            if (animator != null)
                animator.SetBool("isMoving", true);

            // 텔레포트 지점으로 직접 이동
            agent.SetDestination(teleportToMaze.transform.position);

            // 텔레포트 지점에 가까워졌는지 확인
            float distanceToTeleport = Vector3.Distance(transform.position, teleportToMaze.transform.position);
            if (distanceToTeleport < teleportTriggerDistance)
            {
                // 텔레포트 트리거
                TriggerTeleport();
            }
        }
    }

    // 대화가 끝났을 때 호출되는 함수
    public void OnDialogueEnd()
    {
        Debug.Log("Dialogue ended. Rabbit moving to teleport point.");

        // 상태 변경
        isMovingToTeleport = true;
        shouldFollowPlayer = false;

        // 텔레포트 지점으로 이동 시작
        if (teleportToMaze != null)
        {
            MoveToTeleportPoint();
        }
        else
        {
            Debug.LogError("Teleport to maze object not found!");
        }
    }

    private void TriggerTeleport()
    {
        Debug.Log("Rabbit reached teleport point!");

        // 애니메이션 정지
        if (animator != null)
            animator.SetBool("isMoving", false);

        // 텔레포트 오브젝트와의 충돌 시뮬레이션
        // 방법 1: 텔레포트 스크립트의 함수 직접 호출
        var teleportScript = teleportToMaze.GetComponent<MonoBehaviour>();
        if (teleportScript != null)
        {
            // 일반적인 텔레포트 스크립트 함수들 시도
            teleportScript.SendMessage("OnTriggerEnter", GetComponent<Collider>(), SendMessageOptions.DontRequireReceiver);
            teleportScript.SendMessage("TeleportObject", gameObject, SendMessageOptions.DontRequireReceiver);
            teleportScript.SendMessage("Teleport", SendMessageOptions.DontRequireReceiver);
        }

        // 방법 2: 직접 충돌 이벤트 발생시키기
        Collider teleportCollider = teleportToMaze.GetComponent<Collider>();
        Collider rabbitCollider = GetComponent<Collider>();

        if (teleportCollider != null && rabbitCollider != null)
        {
            // 텔레포트 오브젝트의 모든 MonoBehaviour에 OnTriggerEnter 메시지 전송
            MonoBehaviour[] scripts = teleportToMaze.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                script.SendMessage("OnTriggerEnter", rabbitCollider, SendMessageOptions.DontRequireReceiver);
            }
        }

        // 잠시 후 플레이어 따라다니기 시작 (텔레포트 처리 시간 고려)
        Invoke("StartFollowingPlayer", 0.5f);
    }

    private void StartFollowingPlayer()
    {
        // 상태 변경
        isMovingToTeleport = false;
        hasTeleported = true;
        shouldFollowPlayer = true;

        Debug.Log("Rabbit will now follow player after teleportation.");
    }

    // 텔레포트 후 호출될 수 있는 함수 (텔레포트 스크립트에서 호출 가능)
    public void OnTeleportComplete()
    {
        Debug.Log("Teleport complete! Rabbit now following player.");
        StartFollowingPlayer();
    }

    // 충돌 감지 (텔레포트 오브젝트와 충돌했을 때)
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == teleportToMaze && isMovingToTeleport && !hasTeleported)
        {
            TriggerTeleport();
        }
    }

    // 외부에서 플레이어 따라다니기 활성화/비활성화
    public void SetFollowPlayer(bool follow)
    {
        shouldFollowPlayer = follow;
        if (animator != null && !follow)
        {
            animator.SetBool("isMoving", false);
            agent.ResetPath();
        }
    }

    // 현재 상태 확인용 프로퍼티들
    public bool IsMovingToTeleport => isMovingToTeleport;
    public bool HasTeleported => hasTeleported;
    public bool IsFollowingPlayer => shouldFollowPlayer;

    // 디버그용: 현재 상태 표시
    void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 120));
            GUILayout.Label($"Moving to Teleport: {isMovingToTeleport}");
            GUILayout.Label($"Has Teleported: {hasTeleported}");
            GUILayout.Label($"Following Player: {shouldFollowPlayer}");
            GUILayout.Label($"Distance to Target: {distance:F2}");
            GUILayout.EndArea();
        }
    }
}