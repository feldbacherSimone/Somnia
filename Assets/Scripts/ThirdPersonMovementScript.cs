using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

//to do
//implement Running and animation
//Implement Jump animation
//Impement Random bored anim, after some time standing still

//Sepreate code into funktions, 
//Create Seperate class/funktion for animator Code   <--- nevermind that's stupid 

public class ThirdPersonMovementScript : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField]private Vector3 respawnCoords;

    [SerializeField] Footstep stepSounds; 

    [SerializeField]
    private float moveSmoothTime = 0.2f;
    private float animationSmoothTime = 0.3f;
    private float centreSmoothTime;

    public CharacterController contoller;
    public Animator animator;

    public Transform cam;
    public Transform groundCheck;

    public float radius;
    public float groundMargin; 
    public LayerMask groundMask;
    public bool isGrounded;
    public bool isNearGround; 
    
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
    private float centreSmoothVel;

    private float currentHeight;

    [SerializeField]
    private float jumpTimeOut = 1;

    private float _jumoTimeOutDelta; 
    [SerializeField]

    public float jumpAniticipationTime = 0.4f;
    float remainingTime;
    [SerializeField] private float deathHeight;
    [SerializeField] private float midpointHeight;

    public float speed; 

    // Start is called before the first frame update
    void Start()
    {
        _jumoTimeOutDelta = jumpTimeOut; 
        if (respawnPoint != null)
            respawnCoords = respawnPoint.position;
        else
            respawnCoords = transform.position; 

        contoller = gameObject.GetComponent<CharacterController>();
        _inputs = gameObject.GetComponent<StarterAssets.StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, radius, groundMask);
        isNearGround = Physics.CheckSphere(groundCheck.position, radius + groundMargin, groundMask);
        if(isGrounded && currentVel.y < 0)
        {
            currentVel.y = -2f;
        }
        Jump();
        Move();
        

        _jumoTimeOutDelta -= Time.deltaTime;
    
    }
    private void LateUpdate()
    {
        if (transform.position.y < deathHeight)
            gameObject.transform.position = respawnCoords; 
    }

    public void AddJumpForce()
    {

        centreSmoothTime = Mathf.Sqrt(jumpHeight * -2 * -gravity) / (gravity*2.5f);
        currentHeight = transform.position.y;
       // StartCoroutine("AdjustMidpoint");     
        currentVel.y = Mathf.Sqrt(jumpHeight * -2 * -gravity);
    }

    private void Move()
    {
         speed = _inputs.sprint ? sprintSpeed : walkSpeed;

        Vector2 moveInput = _inputs.move;
        currentInput =  Vector2.SmoothDamp(currentInput, moveInput, ref smoothMoveVelocity, moveSmoothTime);
        
        Vector3 direction = new Vector3(currentInput.x, 0f, currentInput.y);
       
        if (direction.magnitude > 0f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //contoller.Move(moveDirection.normalized * speed * Time.deltaTime * currentInput.magnitude);
            contoller.Move(moveDirection.normalized * speed * Time.deltaTime * currentInput.magnitude + currentVel * Time.deltaTime);
            // this is a fucking godsent thank you gamedev stackexchange user Kévin Grandjean 

            currentBlend = Mathf.SmoothDamp(currentBlend, (_inputs.move.magnitude * speed) / sprintSpeed, ref smoothBlendVel, animationSmoothTime);
            currentBlend = Mathf.Clamp(currentBlend, 0, 1);
            //print(currentBlend); 
            animator.SetFloat("Blend", currentBlend);
            stepSounds.setCurretnVol(currentBlend); 
        }
        else
        {
            contoller.Move( currentVel * Time.deltaTime);
        }
    }
    private void Jump()
    {
        


        currentVel.y -= gravity * Time.deltaTime;
        

        //contoller.Move(currentVel * Time.deltaTime);

        if (_inputs.jump && isGrounded && _jumoTimeOutDelta <= 0.0f)
        {
            _jumoTimeOutDelta = jumpTimeOut;
            remainingTime = jumpAniticipationTime; 
            isJumping = true;
            Invoke("AddJumpForce", jumpAniticipationTime);
        }
        else
        {
            isJumping = false;
            _inputs.jump = false;
        }

        animator.SetBool("isJumping", isJumping);
        animator.SetBool("Grounded", isNearGround);
    }
    
    //this took me over 3 hours just to get abandoned in the end 
    //Í am going more bald by the second 

   IEnumerator AdjustMidpoint()
    {

        while (transform.position.y < currentHeight + jumpHeight)
            {
            print("aaaaaaaaaaa");
            contoller.center = new Vector3(contoller.center.x, Mathf.SmoothDamp(contoller.center.y, midpointHeight, ref centreSmoothVel, centreSmoothTime), contoller.center.z);
              
            if (contoller.center.y >= midpointHeight- 0.05f)
            {
                StartCoroutine("AdjustCentreBack");
                StopCoroutine("AdjustMidpoint");
            }
            yield return null;
        }
    }
    IEnumerator AdjustCentreBack()
    {

        while (contoller.center.y >= 0)
        {
            print("bbbb");
            contoller.center = new Vector3(contoller.center.x, Mathf.SmoothDamp(contoller.center.y, 0f, ref centreSmoothVel, centreSmoothTime), contoller.center.z);
            if (contoller.center.y <= 0.001f)
            {
                StopAllCoroutines();
            }
            yield return null;
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
 
    {
        

      
        print("collision"); 
        if (hit.gameObject.CompareTag("Grass") && isGrounded)
        {
            print("grass"); 
            stepSounds.isGrass = true;
        }
        else
            stepSounds.isGrass = false;
    }
}
