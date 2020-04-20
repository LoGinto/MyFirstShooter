using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : MonoBehaviour
{
    public GameObject firstDestination;
    Animator animator;
    AudioSource audioSource;
    NavMeshAgent agent;
    [Space(2)]
    
    
   // Health animalHealth;
    [Header("Sounds of animal")]
    [SerializeField] AudioClip[] attackSounds;
    [Space(5)]
    [Header("Enemies of animal")]
    [SerializeField] Transform animalEnemy = null;
    //[SerializeField] Transform otherEnemy;//test purposes
    [Space(5)]
    [Header("Distance and other variables")]
    public float hearingDistance = 4f;
    //public float chaseDistance = 2f;
    public float attackDistance = 2f;
    public float travelAway = 6f;
    public bool isCarnivore = false;
    public float damage = 3f;
    [Space(2)]
    [Header("SpeedVars")]
    public float speed = 3f;
    public float runningSpeed = 5f;  
    [Header("TimeBetweenAttacks")]
    public float timeBetweenAttacks = 1f;
    //**********************************************************************//d
    private Transform SearchAndFindByTag()
    {
        //find,assign and return
        
        GameObject[] tagArr = GameObject.FindGameObjectsWithTag("Animal");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
            foreach (GameObject taggedObject in tagArr)
            {
                if (Vector3.Distance(transform.position, taggedObject.transform.position) <= hearingDistance && taggedObject != gameObject)
                {
                    animalEnemy = taggedObject.transform;
                    break;
                }
                else
                {
                    continue;
                }

            }
        return animalEnemy;
        }
       
    



    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        //animalHealth = GetComponent<Health>();
        agent.speed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        SearchAndFindByTag();
        if (!isOnSelectedDistanceToEnemy(hearingDistance))
        {
            AnimalNormalBehavior();
        }
        else
        {
            if (isCarnivore == false)
            {
                ScaredBehaviour();
            }
            else
            {
                AgressiveBehaviour();
            }
        }
        
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hearingDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        
        
        
    }
    
    private void AnimalNormalBehavior()
    {
        
            agent.SetDestination(firstDestination.transform.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("Idle", false);
            //maybe add idle too? 
        
    }
    private void ScaredBehaviour()
    {
        
            //for now only distance then I will do some stuff with getting damage
            animator.SetBool("isWalking", false);
            //animator.SetBool("isEating", false);
            animator.SetBool("Idle", false);
            animator.SetBool("isRunning", true);//running anim
            agent.speed = runningSpeed;
            Vector3 dirToPlayer = transform.position - animalEnemy.transform.position;
            Vector3 fleePos = transform.position + dirToPlayer;     
            agent.SetDestination(fleePos);
        Debug.Log("Running away from " + animalEnemy.name);
        
    }
    private void AgressiveBehaviour()
    {
        //applicable to spider.Problem is that I don't have many animations
        transform.LookAt(animalEnemy);
        Chase();
        if (isOnSelectedDistanceToEnemy(attackDistance))
        {
            Attack();
        }
        else
        {
            if (isOnSelectedDistanceToEnemy(hearingDistance) && !isOnSelectedDistanceToEnemy(attackDistance))
            {
                Chase();
            }
        }

    }

    private void Attack()
    {
        transform.LookAt(animalEnemy);
        agent.isStopped = true;
        animator.speed = 1;
        agent.speed = runningSpeed;
        animator.SetBool("isWalking", false);
        StartCoroutine("AttackAnim");
        Debug.Log(gameObject.name + " Attacked " + animalEnemy.name);
        

    }

    private void Chase()
    {
        animator.SetBool("isWalking", true);
        agent.speed = runningSpeed;
        agent.isStopped = false;
        agent.SetDestination(animalEnemy.position);
        agent.destination = animalEnemy.position;
    }

    IEnumerator AttackAnim()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(timeBetweenAttacks);
        animator.ResetTrigger("Attack");
    }
    private bool isOnSelectedDistanceToEnemy(float distance)
    {
        return Vector3.Distance(transform.position, animalEnemy.position) <= distance;
        
    }
   
}
