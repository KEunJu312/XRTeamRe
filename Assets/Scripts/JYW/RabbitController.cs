using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RabbitController : MonoBehaviour
{
    [Header("Follow Settings")]
    public float FollowDistance = 10.0f;
    public Transform target; // �÷��̾� Transform

    [Header("Teleport Settings")]
    public GameObject teleportToMaze; // teleport to maze ������Ʈ
    public float teleportTriggerDistance = 1.5f; // �ڷ���Ʈ Ʈ���� �Ÿ�

    [Header("State Control")]
    public bool shouldFollowPlayer = false; // �÷��̾ ����ٴ��� ����
    public bool isMovingToTeleport = false; // �ڷ���Ʈ �������� �̵� ������

    private NavMeshAgent agent;
    private Animator animator;
    private float distance;
    private bool hasTeleported = false;

    // ���� AITarget�� ������ ����
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // target �ڵ� ã�� (�������� ���� ���)
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
        }

        // teleport to maze ������Ʈ �ڵ� ã�� (�������� ���� ���)
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
            // �ڷ���Ʈ �������� �̵�
            MoveToTeleportPoint();
        }
        else if (shouldFollowPlayer && hasTeleported)
        {
            // ���� AITarget �������� �÷��̾� ����ٴϱ�
            FollowPlayerLogic();
        }
        else if (shouldFollowPlayer && !isMovingToTeleport)
        {
            // ��ȭ �� �⺻ ���¿����� �÷��̾� ����ٴϱ� (�ʿ��� ���)
            FollowPlayerLogic();
        }
    }

    // ���� AITarget�� ������ �÷��̾� ����ٴϱ� ����
    private void FollowPlayerLogic()
    {
        distance = Vector3.Distance(agent.transform.position, target.position);

        if (distance > FollowDistance)
        {
            if (animator != null)
                animator.SetBool("isMoving", true);

            // ���� AITarget�� ������ ������� ��ǥ ���� ���
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

    // �ڷ���Ʈ �������� �̵��ϴ� ����
    private void MoveToTeleportPoint()
    {
        if (teleportToMaze != null && agent != null)
        {
            // �ִϸ��̼� ���� ������Ʈ
            if (animator != null)
                animator.SetBool("isMoving", true);

            // �ڷ���Ʈ �������� ���� �̵�
            agent.SetDestination(teleportToMaze.transform.position);

            // �ڷ���Ʈ ������ ����������� Ȯ��
            float distanceToTeleport = Vector3.Distance(transform.position, teleportToMaze.transform.position);
            if (distanceToTeleport < teleportTriggerDistance)
            {
                // �ڷ���Ʈ Ʈ����
                TriggerTeleport();
            }
        }
    }

    // ��ȭ�� ������ �� ȣ��Ǵ� �Լ�
    public void OnDialogueEnd()
    {
        Debug.Log("Dialogue ended. Rabbit moving to teleport point.");

        // ���� ����
        isMovingToTeleport = true;
        shouldFollowPlayer = false;

        // �ڷ���Ʈ �������� �̵� ����
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

        // �ִϸ��̼� ����
        if (animator != null)
            animator.SetBool("isMoving", false);

        // �ڷ���Ʈ ������Ʈ���� �浹 �ùķ��̼�
        // ��� 1: �ڷ���Ʈ ��ũ��Ʈ�� �Լ� ���� ȣ��
        var teleportScript = teleportToMaze.GetComponent<MonoBehaviour>();
        if (teleportScript != null)
        {
            // �Ϲ����� �ڷ���Ʈ ��ũ��Ʈ �Լ��� �õ�
            teleportScript.SendMessage("OnTriggerEnter", GetComponent<Collider>(), SendMessageOptions.DontRequireReceiver);
            teleportScript.SendMessage("TeleportObject", gameObject, SendMessageOptions.DontRequireReceiver);
            teleportScript.SendMessage("Teleport", SendMessageOptions.DontRequireReceiver);
        }

        // ��� 2: ���� �浹 �̺�Ʈ �߻���Ű��
        Collider teleportCollider = teleportToMaze.GetComponent<Collider>();
        Collider rabbitCollider = GetComponent<Collider>();

        if (teleportCollider != null && rabbitCollider != null)
        {
            // �ڷ���Ʈ ������Ʈ�� ��� MonoBehaviour�� OnTriggerEnter �޽��� ����
            MonoBehaviour[] scripts = teleportToMaze.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                script.SendMessage("OnTriggerEnter", rabbitCollider, SendMessageOptions.DontRequireReceiver);
            }
        }

        // ��� �� �÷��̾� ����ٴϱ� ���� (�ڷ���Ʈ ó�� �ð� ���)
        Invoke("StartFollowingPlayer", 0.5f);
    }

    private void StartFollowingPlayer()
    {
        // ���� ����
        isMovingToTeleport = false;
        hasTeleported = true;
        shouldFollowPlayer = true;

        Debug.Log("Rabbit will now follow player after teleportation.");
    }

    // �ڷ���Ʈ �� ȣ��� �� �ִ� �Լ� (�ڷ���Ʈ ��ũ��Ʈ���� ȣ�� ����)
    public void OnTeleportComplete()
    {
        Debug.Log("Teleport complete! Rabbit now following player.");
        StartFollowingPlayer();
    }

    // �浹 ���� (�ڷ���Ʈ ������Ʈ�� �浹���� ��)
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == teleportToMaze && isMovingToTeleport && !hasTeleported)
        {
            TriggerTeleport();
        }
    }

    // �ܺο��� �÷��̾� ����ٴϱ� Ȱ��ȭ/��Ȱ��ȭ
    public void SetFollowPlayer(bool follow)
    {
        shouldFollowPlayer = follow;
        if (animator != null && !follow)
        {
            animator.SetBool("isMoving", false);
            agent.ResetPath();
        }
    }

    // ���� ���� Ȯ�ο� ������Ƽ��
    public bool IsMovingToTeleport => isMovingToTeleport;
    public bool HasTeleported => hasTeleported;
    public bool IsFollowingPlayer => shouldFollowPlayer;

    // ����׿�: ���� ���� ǥ��
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