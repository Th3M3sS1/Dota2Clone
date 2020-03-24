using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;
    //public float moveSpeed;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();        
    }

    private void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        if(GetComponent<PlayerController>())
            agent.speed = GetComponent<PlayerController>().movementSpeed;

        else if(GetComponent<PlayerControllerRange>())
            agent.speed = GetComponent<PlayerControllerRange>().movementSpeed;

        agent.SetDestination(point);
    }

    public void FollowTarget(LivingEntityManager newTarget)
    {
        if (GetComponent<PlayerController>())
            agent.stoppingDistance = GetComponent<PlayerController>().attackRange;

        else if (GetComponent<PlayerControllerRange>())
            agent.stoppingDistance = GetComponent<PlayerControllerRange>().attackRange;

        agent.updateRotation = false;
        target = newTarget.transform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookROtation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookROtation, Time.deltaTime * 5);
    }
}
