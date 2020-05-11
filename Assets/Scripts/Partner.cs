using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Partner : MonoBehaviour
{
    [Header("Too lazy to sort")]
    public float speed;
    Vector3 direction;
    public float runningSpeed;
    public float stealthSpeed;
    public float hireDistance = 3f;
    public float hearingDistance;
    public float standAwayFromPlayer;
    private Animator animator;
    public float fieldOfViewAngle = 110f;
    CommandAI command;
    public float damage = 4f;
    private NavMeshAgent nav;
    private bool enemyInsight;
    private bool agressiveBehaviour;
    private bool waitHere;
    private bool followMe;
    public Transform enemyOfAI;
    private bool hookedUp = false;
    public float timeBetweenAttacks = 0.4f;
    private Vector3 initialPosition;
    AudioSource audioSource;
    [SerializeField] AudioClip okaySound;
    FirstPersonMovement playerMovement;
    GameObject player;
    SphereCollider sphereCollider;
    Transform placeHolder;
    private void Start()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<FirstPersonMovement>();
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
        command = player.GetComponent<CommandAI>();
        sphereCollider = GetComponent<SphereCollider>();
        placeHolder = GameObject.FindGameObjectWithTag("PlaceHolder").transform;
        enemyOfAI = placeHolder;

    }

    private void Update()
    {
        if (enemyOfAI == null)
        {
            enemyOfAI = GameObject.FindGameObjectWithTag("PlaceHolder").transform;
        }
        if (enemyOfAI != placeHolder || enemyOfAI != null)
        {
            DealDamage();
        }
        SearchAndFindByTag();
        HookUp();
        try
        {
            StopSneakGo();
        }
        catch
        {
            Debug.Log("I don't give a fuck");
        }
        Follow();
        Wait();
        if (isOnSelectedDistanceToPlayer(standAwayFromPlayer) && playerMovement.GetStealth())
        {
            animator.SetBool("Sneak", false);
            animator.SetBool("Crouch", true);

        }
    }
    void Follow()
    {
        if (waitHere == false && followMe == true && hookedUp && !isOnSelectedDistanceToPlayer(standAwayFromPlayer))
        {
            nav.isStopped = false;
            nav.SetDestination(player.transform.position);
            if (playerMovement.GetStealth())
            {
                nav.speed = stealthSpeed;
                animator.SetBool("Sneak", true);
                animator.SetBool("Crouch", false);

            }

        }
        if (isOnSelectedDistanceToPlayer(standAwayFromPlayer))
        {
            nav.isStopped = true;
            if (playerMovement.GetStealth() && playerMovement.IsStanding() == false)
            {
                animator.SetBool("Sneak", false);
                animator.SetBool("Crouch", true);
            }
        }
    }
    void Wait()
    {
        if (waitHere == true && followMe == false && hookedUp)
        {
            nav.isStopped = true;
            animator.SetBool("Idle", true);
            Debug.Log(gameObject.name + " waits");
        }
    }
    private void StopSneakGo()
    {
        if (hookedUp == false)
        {
            nav.SetDestination(initialPosition);
        }

        //if (playerMovement.IsStanding() || isOnSelectedDistanceToPlayer(standAwayFromPlayer))
        //{
        //    nav.isStopped = true;
        //}
        //else
        //{
        //    nav.isStopped = false;
        //}
        if (playerMovement.GetStealth() && playerMovement.IsStanding() && hookedUp)
        {
            animator.SetBool("Crouch", true);
            animator.SetBool("Idle", false);
            Debug.Log(gameObject.name + " should crouch");
        }
        if (!isOnSelectedDistanceToPlayer(standAwayFromPlayer) && playerMovement.GetStealth() && hookedUp)
        {
            animator.SetBool("Crouch", false);
        }
        if (playerMovement.IsStanding() && hookedUp && !playerMovement.GetStealth())
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Crouch", false);
            animator.SetBool("Sneak", false);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Animal") || other.gameObject.CompareTag("Zombie"))
        {
            enemyInsight = false;
            direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < fieldOfViewAngle * 0.5f)
            {
                AllySight();
            }
        }
    }
    void AllySight()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCollider.radius))
        {
            if (hit.collider != null)
            {
                Debug.DrawLine(transform.position + transform.up, hit.point, Color.red);
                if (hit.collider.CompareTag("Animal") || hit.collider.CompareTag("Zombie"))
                {
                    Debug.Log("I see an enemy");
                    nav.isStopped = true;
                    enemyOfAI = hit.transform;
                    enemyInsight = true;
                    DealDamage();
                }
            }
            else
            {
                Debug.DrawLine(transform.position + transform.up, transform.position + transform.forward * sphereCollider.radius, Color.blue);
                nav.isStopped = false;
                enemyInsight = false;
            }
        }
    }

    private void DealDamage()
    {
        transform.LookAt(Vector3.Scale(enemyOfAI.position, new Vector3(0, 1, 1)));

        StartCoroutine("AttackPause");
        try
        {
            enemyOfAI.GetComponent<Health>().TakeDamage(damage);
        }
        catch
        {
            return;
        }
        if (enemyOfAI.GetComponent<Health>().Died())
        {
            enemyOfAI = null;
            nav.isStopped = false;

            StopCoroutine("AttackPause");
        }
    }

    IEnumerator AttackPause()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(timeBetweenAttacks);
        animator.ResetTrigger("Attack");
    }
    void HookUp()
    {
        //for test purpose I will just come near and press button to hire
        if (isOnSelectedDistanceToPlayer(hireDistance) && Input.GetKeyDown(KeyCode.V))
        {
            hookedUp = !hookedUp;
            transform.LookAt(Vector3.Scale(player.transform.position, new Vector3(0, 1, 1)));
            audioSource.PlayOneShot(okaySound);
            if (hookedUp)
            {
                command.SetPartner(this.gameObject);

                command.canControl = true;
            }
            else
            {
                command.SetPartner(null);
                command.canControl = false;
            }
        }
    }
    private bool isOnSelectedDistanceToPlayer(float distance)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= distance;
    }
    private Transform SearchAndFindByTag()
    {
        //find,assign and return

        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");

        if (enemyOfAI == null || enemyOfAI == placeHolder)
        {
            foreach (GameObject taggedObject in animals)//iteration through animals
            {
                if (Vector3.Distance(transform.position, taggedObject.transform.position) <= hearingDistance && taggedObject != gameObject && taggedObject.GetComponent<Health>().Died() == false)
                {
                    enemyOfAI = taggedObject.transform;
                    break;
                }
                else
                {
                    enemyOfAI = null;
                    continue;

                }

            } //iteration through zombies
            foreach (GameObject taggedObject in zombies)
            {
                if (Vector3.Distance(transform.position, taggedObject.transform.position) <= hearingDistance && taggedObject != gameObject && taggedObject.GetComponent<Health>().Died() == false)
                {
                    enemyOfAI = taggedObject.transform;
                    break;
                }
                else
                {
                    enemyOfAI = null;
                    continue;

                }

            }
        }

        return enemyOfAI;
    }  
    public bool IsHooked()
    {
        return hookedUp;
    }
    public void SetAgressive(bool value)
    {
        agressiveBehaviour = value;
    }
    public bool GetAgressionState()
    {
        return agressiveBehaviour;
    }
    public void SetFollowing(bool value)
    {
        this.followMe = value;
    }
    public void SetWaiting(bool value)
    {
        this.waitHere = value;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hireDistance);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, standAwayFromPlayer);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hearingDistance);
    }
}