using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] EnemyWays enemyWay;
    int wayIndex = 0;
    Enemy enemy;
    float timeSinceArrived = 0;
    public float nextPointTime = 3f;
    public float maxDistanceFromWaypoint = 1.5f;
    Vector3 currentPos;
    Animator animator;
    NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        currentPos = transform.position;
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceArrived += Time.deltaTime;
        Patrol();
    }

    private void Patrol()
    {
        if (!enemy.SightedPlayer())
        {
            //Loop through the points 
            Vector3 nextPos = currentPos;

            if (isOnwayPoint())
            {
                timeSinceArrived = 0;
                NextPoint();
            }
            nextPos = CurrentWayPointPos();
            if (timeSinceArrived > nextPointTime)
            {
                MoveTo(nextPos);

            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("Idle", true);
            }
        }
    }

    void MoveTo(Vector3 destination)
    {
        nav.destination = destination;
        nav.isStopped = false;

        if (!enemy.SightedPlayer())
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("Idle", false);
        }
    }
    private void NextPoint()
    {
        wayIndex = enemyWay.GetNextIndexInLoop(wayIndex);
    }
    private Vector3 CurrentWayPointPos()
    {
        return enemyWay.GetEndpoint(wayIndex);
    }
   public bool isOnwayPoint()
    {
        
        float distanceToPath = Vector3.Distance(transform.position, CurrentWayPointPos());
        return distanceToPath < maxDistanceFromWaypoint;
    }
}
