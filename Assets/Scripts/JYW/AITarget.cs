using OVR.OpenVR;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AITarget : MonoBehaviour
{
    public float FollowDistance = 2.0f;
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
            
            agent.destination = target.position;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }


}
