using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstPersonMovement : MonoBehaviour
{
    CharacterController characterController;
    float charHeight_initial;
    public float speed = 11f;
    public float jumpHeight = 3f;
    AudioSource audioSource;
    public AudioClip breathRecoveryClip;
    public float crouchSpeed = 4f;
    private float initial_speed;
    Vector3 velocity;
    public float gravityForce = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float runSpeed = 14f;
    public Slider staminaSlider;
    public bool stealth = false;
    bool isStanding;
    float init_speed;
    [SerializeField] float runRegenTime = 10;
    [SerializeField]float maxrunDuration = 10;
    public bool isRunning = false;
    //public float maxRunDuration = 12f;


    bool canRun = true;
    bool tryRun = false;


    // Start is called before the first frame update
    void Start()
    {
        initial_speed = speed;
        characterController = GetComponent<CharacterController>();
        charHeight_initial = characterController.height;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("CheckRunEnergy");
    }


    // Update is called once per frame
    void Update()
    {
        staminaSlider.value = CalculateStamina();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        Vector3 movementVector = transform.right * xAxis + transform.forward * zAxis;
        characterController.Move(movementVector * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityForce);
        }
        velocity.y += gravityForce * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        Crouch();
        Run();
        if (characterController.velocity.x == 0 && characterController.velocity.y == 0)
        {
            isStanding = true;
        }
        else
        {
            isStanding = false;
        }

    }
    private void FixedUpdate()
    {
      // Run();
        print(canRun);
    }
    private void Run()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //if (runDuration <= maxRunDuration)
            //{
            //speed = runSpeed;

            //runDuration += 1 * Time.deltaTime;

            //isRunning = true;
            tryRun = true;
            if (canRun)
            {
                speed = runSpeed;
                isRunning = true;
            }
            else if (!canRun)
            {
                speed = initial_speed;
                isRunning = false;
            }
        }
        else
        {
            tryRun = false;
            isRunning = false;
        }
    }

    
    //if (!(Input.GetKey(KeyCode.LeftShift))|| runDuration >= maxRunDuration)
    //{
    //    if (runDuration > 0)
    //    {
    //        speed = initial_speed;
    //        runDuration -= 1 * Time.deltaTime;
    //        isRunning = false;
    //    }
    //}

    
    IEnumerator CheckRunEnergy()
    {
        while (true)
        {
            if (tryRun)
            {
                while (canRun)
                {
                    speed = runSpeed;
                    yield return new WaitForSeconds(maxrunDuration);
                    canRun = false;
                    audioSource.PlayOneShot(breathRecoveryClip, 1.0f);
                }
                while (!canRun)
                {
                    yield return new WaitForSeconds(runRegenTime);
                    canRun = true;
                }
            }


            yield return new WaitForEndOfFrame();
        }
    }
    private float CalculateStamina()
    {
        return runRegenTime / 100;//not sure how to change this
    }
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            stealth = !stealth;
        }
        if (stealth)
        {
            speed = crouchSpeed;
            // characterController.height = Mathf.Lerp(characterController.height,charHeight_initial / 2,0.5f);
            characterController.height = charHeight_initial / 2;
        }
        else
        {
            speed = initial_speed;
            characterController.height = charHeight_initial;
        }
    }
    public bool PlayerIsRunning()
    {
        return isRunning;
    }
    public bool GetStealth()
    {
        return stealth;
    }
    public bool IsStanding()
    {
        return isStanding;
    }
}
