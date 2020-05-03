using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    CharacterController characterController;
    float charHeight_initial; 
    public float speed = 11f;
    public float jumpHeight = 3f;
    public float crouchSpeed = 4f;
    private float initial_speed;
    Vector3 velocity;
    public float gravityForce = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool stealth = false;
    bool isStanding;
    // Start is called before the first frame update
    void Start()
    {
        initial_speed = speed;
        characterController = GetComponent<CharacterController>();
        charHeight_initial = characterController.height;
    }
    

    // Update is called once per frame
    void Update()
    {
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
        if(characterController.velocity.x == 0 && characterController.velocity.y == 0)
        {
            isStanding = true;
        }
        else
        {
            isStanding = false;
        }

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
            characterController.height = charHeight_initial/2;
        }
        else
        {
            speed = initial_speed;
            characterController.height = charHeight_initial;
        }
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
