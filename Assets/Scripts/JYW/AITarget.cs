using OVR.OpenVR;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AITarget : MonoBehaviour
{
    public float FollowDistance = 10.0f;
    public Transform target;

    private NavMeshAgent agent;
    private Animator animator;
    private float distance;

   

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
      
        
    }

    void Update()
    { 
        distance = Vector3.Distance(agent.transform.position, target.position);

        if (distance > FollowDistance)
        { 
            animator.SetBool("isMoving", true);

            // agent.destination = target.position;
            Vector3 direction = (agent.transform.position - target.position).normalized;
            Vector3 desiredPosition = target.position + direction * FollowDistance;

            agent.SetDestination(desiredPosition);
        }
        else
        {
            animator.SetBool("isMoving", false);
            agent.ResetPath();
        }
    }


}
