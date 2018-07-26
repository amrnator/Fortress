using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//processes movement of the agent
public class DwarfMotion : MonoBehaviour
{

    Transform target; //transform we're targeting and moving towards

    NavMeshAgent agent; // agent for this object

    public int wanderRadius = 3; //radius of circle from curr position where random point will be picked

    bool run = true;
    bool isWandering = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }

        //wander around if thats the job
        if (isWandering && run)
        {
            Wander();
        }
    }

    public void MoveToPoint(Vector3 dest)
    {
        agent.SetDestination(dest);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;
        target = newTarget.transform;
    }

    public void StopFollowTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.localRotation, lookRot, Time.deltaTime * 5f);
    }

    public void startWander()
    {
        isWandering = true;    
    }

    public void stopWander()
    {
        isWandering = false;
    }

    //pick random spots around the agent and walk to them
    public void Wander()
    {
        //wait
        run = false;
        StartCoroutine(pause(4.0f));

        //pick a random vector somewhere around the player, make sure it's on the mesh!
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        Vector3 finalPosition = hit.position;

        //set destination
        MoveToPoint(finalPosition);
    }

    private IEnumerator pause(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        run = true;
    }
}

