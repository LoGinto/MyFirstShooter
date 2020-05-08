using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] EnemyWays enemyWay = null;
    int wayIndex = 0;
    Vector3 currentPos;
    Animator animator;
    NavMeshAgent navMeshagent;
    float timeSinceArrived = 0;
    public float nextPointTime = 3f;
    public float maxDistanceFromWaypoint = 5f;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        navMeshagent = GetComponent<NavMeshAgent>();
        currentPos = transform.position;
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceArrived += Time.deltaTime;
        PatrolPath();
    }

    private void PatrolPath()
    {
        Vector3 nextPos = currentPos;
        if (!enemy.SightedPlayer())
        {
            if (ISonWayPoint())
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("Idle", true);

                NextPoint();
                
                nextPos = CurrentWayPointPos();

                if (timeSinceArrived > nextPointTime)
                {
                    timeSinceArrived = 0;
                    MoveTo(nextPos);
                }
            }
        }
    }
    IEnumerator Stay()
    {
        yield return new WaitForSeconds(nextPointTime);
    }

    private void NextPoint()
    {
        wayIndex = enemyWay.GetNextIndexInLoop(wayIndex);
    }
    private Vector3 CurrentWayPointPos()
    {
        return enemyWay.GetEndpoint(wayIndex);
    }

    void MoveTo(Vector3 newDestination)
    {
        navMeshagent.destination = newDestination;
        navMeshagent.SetDestination(newDestination);
        navMeshagent.isStopped = false; 
        animator.SetBool("isWalking", true);
        animator.SetBool("Idle", false);
        
    }
   public bool ISonWayPoint()
    {
        float distanceToPath = Vector3.Distance(transform.position, CurrentWayPointPos());
        return distanceToPath < maxDistanceFromWaypoint;

    }
    
}
    