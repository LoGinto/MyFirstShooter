using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Partner : MonoBehaviour
{
    public float speed;
    public float runningSpeed;
    public float stealthSpeed;
    public float hireDistance = 3f;
    public float standAwayFromPlayer;
    private Animator animator;
    CommandAI command;
    private NavMeshAgent nav;
    private bool enemyInsight;
    private bool agressiveBehaviour;
    private bool waitHere;
    private bool followMe;
    private bool hookedUp = false;
    private Vector3 initialPosition;
    AudioSource audioSource;
    [SerializeField] AudioClip okaySound;
    FirstPersonMovement playerMovement;
    GameObject player;
    private void Start()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<FirstPersonMovement>();
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
        command = player.GetComponent<CommandAI>();
    }

    private void Update()
    {
        HookUp();
        StopSneakGo();
        Follow();
        Wait();
        if (isOnSelectedDistanceToPlayer(standAwayFromPlayer)&&playerMovement.GetStealth())
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
                animator.SetBool("Sneak",true);
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
        if(waitHere == true && followMe == false && hookedUp)
        {
            nav.isStopped = true;
            animator.SetBool("Idle",true);
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
        if(!isOnSelectedDistanceToPlayer(standAwayFromPlayer) && playerMovement.GetStealth() && hookedUp)
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
    public bool IsHooked()
    {
        return hookedUp;
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
    }
}