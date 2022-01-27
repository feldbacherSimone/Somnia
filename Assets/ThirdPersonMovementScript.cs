using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//to do
//implement Running and animation
//Implement Jump animation
//Impement Random bored anim, after some time standing still

//Sepreate code into funktions, 
//Create Seperate class/funktion for animator Code

public class ThirdPersonMovementScript : MonoBehaviour
{
    [SerializeField]
    private float moveSmoothTime = 0.2f;
    private float animationSmoothTime = 0.3f; 

    public CharacterController contoller;
    public Animator animator;

    public Transform cam;
    public Transform groundCheck;

    public float radius;
    public LayerMask groundMask;
    public bool isGrounded; 
    
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f; 
    public float jumpHeight = 10f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVel;

    public Vector3 currentVel;
    public float gravity = -9.81f;

    private bool isJumping;

    private StarterAssets.StarterAssetsInputs _inputs;

    private Vector2 currentInput;
    private Vector2 smoothMoveVelocity;

    private float currentBlend;
    private float smoothBlendVel;

    public float jumpAniticipationTime; 


    // Start is called before the first frame update
    void Start()
    {
        contoller = gameObject.GetComponent<CharacterController>();
        _inputs = gameObject.GetComponent<StarterAssets.StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, radius, groundMask);
        if(isGrounded && currentVel.y < 0)
        {
            currentVel.y = -2f;
        }

        Move();
        Jump();
    
    }

    public void AddJumpForce()
    {
        currentVel.y = Mathf.Sqrt(jumpHeight * -2 * -gravity);
    }

    private void Move()
    {
        float speed = _inputs.sprint ? sprintSpeed : walkSpeed;

        Vector2 moveInput = _inputs.move;
        currentInput =  Vector2.SmoothDamp(currentInput, moveInput, ref smoothMoveVelocity, moveSmoothTime);
        
        Vector3 direction = new Vector3(currentInput.x, 0f, currentInput.y);
       
        if (direction.magnitude > 0f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            contoller.Move(moveDirection.normalized * speed * Time.deltaTime * currentInput.magnitude);

            currentBlend = Mathf.SmoothDamp(currentBlend, (_inputs.move.magnitude * speed) / sprintSpeed, ref smoothBlendVel, animationSmoothTime);
            currentBlend = Mathf.Clamp(currentBlend, 0, 1);
            print(currentBlend); 
            animator.SetFloat("Blend", currentBlend);
        }
    }
    private void Jump()
    {
        currentVel.y -= gravity * Time.deltaTime;
        contoller.Move(currentVel * Time.deltaTime);
        if (_inputs.jump && isGrounded)
        {
            isJumping = true;
            //currentVel.y = Mathf.Sqrt(jumpHeight * -2 * -gravity);
            Invoke("AddJumpForce", 0.15f);
            
            print(currentVel);
        }
        else
        {
            isJumping = false;
            _inputs.jump = false;
        }
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("Grounded", isGrounded);
    }
}
