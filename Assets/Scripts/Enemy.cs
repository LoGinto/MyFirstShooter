using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{

    [Header("Seeing vars")]
    public float maxDistance;
    public Transform eyesPoint;
    // [SerializeField] Transform firstPoint;//to return after chase
    [Space(3)]
    [Header("Chase vars")]
    public bool playerInSight;
    public float runningSpeed;
    
    public Transform enemyOfenemy;
    [Header("Attack vars")]
    public float attackDistance = 2f;
    public float timeBetweenAttacks = 1f;
    private NavMeshAgent nav;
    //private Collider col;
    Animator animator;
    GameObject player;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Start()
    {
        
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        Physics.queriesHitBackfaces = false;
        enemyOfenemy = player.transform;
        
    }

    private void Update()
    {
        EnemySight();
        Chase();
        StopOnAttack();
    }
    void StopOnAttack()
    {
        if (isOnSelectedDistanceToPlayer(attackDistance)&&playerInSight)
        {
            animator.SetBool("isRunning", false);
            nav.isStopped = true;
            StartCoroutine("AttackAnim");
        }
        if (!isOnSelectedDistanceToPlayer(attackDistance) && playerInSight)
        {
            Chase();
        }
    }
    IEnumerator AttackAnim()
    {
        animator.SetTrigger("Attack");
        
        yield return new WaitForSeconds(timeBetweenAttacks);
        animator.ResetTrigger("Attack");
       
    }
    private void EnemySight()
    {
        RaycastHit hit;

        if (Physics.Raycast(eyesPoint.position, eyesPoint.forward, out hit, maxDistance))
        {
            if (hit.collider != null)
            {
                Debug.DrawLine(eyesPoint.position, hit.point, Color.red);
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Ally"))
                {
                    Debug.Log("I see player or player's Ally");
                    enemyOfenemy = hit.transform;
                    playerInSight = true;
                }
            }
            else
            {
                Debug.DrawLine(eyesPoint.position, eyesPoint.position + eyesPoint.forward * maxDistance, Color.blue);
                playerInSight = false;
            }
        }


    }
    public bool SightedPlayer()
    {
        return playerInSight;
    }
    private void Chase()
    {

        if (playerInSight)
        {
            
            transform.LookAt(enemyOfenemy);
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("Idle", false);
            nav.speed = runningSpeed;
            nav.isStopped = false;
            nav.SetDestination(enemyOfenemy.position);
            nav.destination = enemyOfenemy.position;
        }
    }
    private bool isOnSelectedDistanceToPlayer(float distance)
    {
        return Vector3.Distance(transform.position, enemyOfenemy.position) <= distance;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}