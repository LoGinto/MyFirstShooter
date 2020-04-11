using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : MonoBehaviour
{
    //Applicable to boar
    //Variables
    Animator animator;
    AudioSource audioSource;
    NavMeshAgent agent;
   // Health animalHealth;
    [Header("Sounds of animal")]
    [SerializeField] AudioClip[] idleSounds;
    [SerializeField] AudioClip[] attackSounds;
    [Space(5)]
    [Header("Enemies of animal")]
    [SerializeField] Transform player;
    //[SerializeField] Transform otherEnemy;//test purposes
    [Space(5)]
    [Header("Distance and other variables")]
    public float hearingDistance = 4f;
    public float attackDistance = 1.6f;
    public float speed = 2f;
    public float timeBetweenAttacks = 1f;
    public float travelAway = 6f;
   //**********************************************************************//
    float timeSinceLastAttack = Mathf.Infinity;//for reload
    bool isDisturbed = false;
    bool isAgressive = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        //animalHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimalNormalBehavior();
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
        if (!isOnSelectedDistanceToPlayer(hearingDistance))
        {
            //animals eat or walk
            StartCoroutine("Breaks");
        }

    }
   IEnumerator Breaks()
    {
        
        yield return new WaitForSeconds(20f);
        WalkAround();
    }
    private void WalkAround()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("Idle", false);
        animator.SetBool("isEating", false);
        Vector3 randomDestination = Random.insideUnitSphere * travelAway;
        randomDestination += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDestination, out hit, travelAway, 1);
        Vector3 finalPosition = hit.position;
        agent.destination = finalPosition;
        if (transform.position == finalPosition)
        {
            agent.isStopped = true;
        }
        
    }

    private  void AnimalAggressiveBehavior()
    {

    }
    private void AnimalScaredBehavior()
    {
        
    }
    private bool isOnSelectedDistanceToPlayer(float distance)
    {
        return Vector3.Distance(transform.position, player.position) <= distance;
        //might apply it to other enemies as well later on
    }

}
