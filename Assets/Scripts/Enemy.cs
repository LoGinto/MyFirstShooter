using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [Header("Seeing vars")]
    public float fieldOfViewAngle = 110f;
    // [SerializeField] Transform firstPoint;//to return after chase
    [Space(3)]
    [Header("Chase vars")]
    public bool playerInSight;
    public float runningSpeed;
    public Transform enemyOfenemy;
    [Header("Attack vars")]
    public float attackDistance = 2f;
    public float timeBetweenAttacks = 1f;
    public Transform attackPoint = null;
    public float attackRadius;
    public float damage;
    public LayerMask hostileLayer;
    //***************************************//
    private NavMeshAgent nav;
    Animator animator;
    GameObject player;
    Vector3 direction;
    SphereCollider sphereCollider;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        Physics.queriesHitBackfaces = false;
        enemyOfenemy = player.transform;

    }

    private void Update()
    {
        if(enemyOfenemy == null)
        {
            enemyOfenemy = player.transform;
            //debug purpose for now        
        }
        //EnemySight();
        if (playerInSight)
        {
            Chase();
        }
        else
        {
            StopCoroutine("AttackAnim");
        }
        StopOnAttack();
    }
    void StopOnAttack()
    {
        if (isOnSelectedDistanceToPlayer(attackDistance) && playerInSight)
        {
            animator.SetBool("isRunning", false);
            nav.isStopped = true;
            StartCoroutine("AttackAnim");
            Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackRadius, hostileLayer);
            foreach (Collider enemy in enemies)
            {
                if (enemy.CompareTag("Ally"))
                {
                    enemy.GetComponent<Health>().TakeDamage(damage);
                    Debug.Log(enemy.name + " took " + damage + " damage");
                    if (enemy.GetComponent<Health>().Died())
                    {
                        enemyOfenemy = player.transform;
                        nav.isStopped = false;
                        playerInSight = false;
                        StopCoroutine("AttackAnim");
                        animator.SetBool("Idle", true);
                        animator.SetBool("isAttacking", false);
                    }
                }
                else if (enemy.CompareTag("Player"))
                {
                    enemy.GetComponent<PlayerHealth>().DealDamageToPlayer(damage);
                    Debug.Log(enemy.name + " took " + damage + " damage");
                    if (enemy.GetComponent<PlayerHealth>().PlayerDied())
                    {
                        enemyOfenemy = null;
                        nav.isStopped = false;
                    }
                }
            }
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ally"))
        {
            playerInSight = false;
            direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if(angle< fieldOfViewAngle * 0.5f)
            {
                EnemySight();
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ally"))
        {
            playerInSight = false;
        }
    }

    private void EnemySight()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCollider.radius))
        {
            if (hit.collider != null)
            {
                Debug.DrawLine(transform.position + transform.up, hit.point, Color.red);
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Ally"))
                {
                    Debug.Log("I see player or player's Ally");
                    enemyOfenemy = hit.transform;
                    playerInSight = true;
                }
            }
            else
            {
                Debug.DrawLine(transform.position + transform.up, transform.position + transform.forward * sphereCollider.radius, Color.blue);
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
            transform.LookAt(Vector3.Scale(enemyOfenemy.position, new Vector3(0, 1, 1)));
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("Idle", false);
            nav.speed = runningSpeed;
            nav.isStopped = false;
            nav.SetDestination(enemyOfenemy.position);
            nav.destination = enemyOfenemy.position;    
    }

    private bool isOnSelectedDistanceToPlayer(float distance)
    {
        return Vector3.Distance(transform.position, enemyOfenemy.position) <= distance;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

}
    