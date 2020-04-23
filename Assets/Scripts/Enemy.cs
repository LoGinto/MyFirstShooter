using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [Header("Usual vars")]
    public float maxDistance; 
    public Transform eyesPoint;
    [SerializeField] Transform firstPoint;//to return after chase
    [Space(3)]
    [Header("Chase vars")]
    public bool playerInSight;
    public float runningSpeed;
    //const float rotationY = 101.072f;
    //public Vector3 personalLastSight;
    public Transform enemyOfenemy;
    //public int layer_mask = LayerMask.GetMask("Player", "Player_Ally");
    private NavMeshAgent nav;
    private Collider col;
    private Animator animator;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        //transform.rotation = Quaternion.Euler(0, rotationY, 0); 
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();
        Physics.queriesHitBackfaces = false;  
        enemyOfenemy = player.transform;
    }

    private void Update()
    {

        EnemySight();
        Chase();
    }

    private void EnemySight()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(eyesPoint.position, eyesPoint.forward, out hitInfo, maxDistance))
        {
            if (hitInfo.collider != null)
            {
                Debug.DrawLine(eyesPoint.position, hitInfo.point, Color.red);
                if (hitInfo.collider.CompareTag("Player")||hitInfo.collider.CompareTag("Ally"))
                {
                    Debug.Log("I see player or player's Ally");
                    enemyOfenemy = hitInfo.transform;
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

}
