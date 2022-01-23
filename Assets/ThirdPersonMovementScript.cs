using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to do
//implement Running and animation
//Implement Jump animation
//Impement Random bored anim, after some time standing still

//Sepreate code into funktions, 
//Create Seperate class/funktion for animator Code

public class ThirdPersonMovementScript : MonoBehaviour
{
    public Transform cam;
    public Transform groundCheck;
    public float jumpHeight = 10;

    public float radius;
    public LayerMask groundMask;
    public bool isGrounded; 

    public CharacterController contoller;
    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVel;

    public Vector3 currentVel;
    public float gravity = -9.81f;

    public Animator animator;

    bool isWalking;
    bool isRunning;
    bool isJumping; 
    // Start is called before the first frame update
    void Start()
    {
        contoller = gameObject.GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, radius, groundMask);
        if(isGrounded && currentVel.y < 0)
        {
            currentVel.y = -2f;
        }

       
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        currentVel.y -= gravity * Time.deltaTime;


        //Jumps if character is grounded. 
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            //currentVel.y = Mathf.Sqrt(jumpHeight * -2 * -gravity);
            Invoke("AddJumpForce",0.15f);
            print(currentVel);
        }
        else
            isJumping = false;

        //move Character 
        contoller.Move(currentVel * Time.deltaTime );     
        //smooths out turns

        if (direction.magnitude > 0f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f); 


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            contoller.Move( moveDirection.normalized * speed * Time.deltaTime);

            isWalking = true;


        }
        else
            isWalking = false;

        if (isWalking && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            speed = 12;
        }

        else
        {
            speed = 6;
            isRunning = false;
        }
           

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }

    public void AddJumpForce()
    {
        currentVel.y = Mathf.Sqrt(jumpHeight * -2 * -gravity);
    }
}
