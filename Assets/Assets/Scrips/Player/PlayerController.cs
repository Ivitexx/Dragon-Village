using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    [Header("Parameters")]
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public float speedRotation;
    public Vector3 moveDirection;

    [Header("Object")]
    public Camera playerCamera;
    public GameObject playerModel;
    public Animator animator;
    private CharacterController characterController;

    [Header("Ground")]
    public bool grounded;
    public Transform groundChecker;
    public LayerMask WhatsGround;

    [Header("Animations")]
    public bool running;
    public bool jumping;

    [Header("KnockBack Parameters")]
    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockBackCounter;
    public Vector2 knockBackPower;

    [Header("Player Model")]
    public GameObject[] PlayerPieces;

    private float ySpeed;
    private float originalStepOffset;
    public bool isAttacking;

    public bool isDefend;
    public GameObject Shield;

    public GameObject HurtBox;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    void Update()
    {
        if(!isKnocking && !isDefend && !isAttacking)
        {
            Movement();
        }

        if(characterController.isGrounded)
        {
            Attack();
            Defend();
        }

        Gravity();
        IsKnocking();
    }
    
    void Movement()
    {
        //float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));

        Jump();
        //moveDirection = AdjustVelocityToSlope(moveDirection);
        //moveDirection.y += yStore;
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection = AdjustVelocityToSlope(moveDirection);
        moveDirection.y += ySpeed;


        characterController.Move(moveDirection * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            running = true;
            animator.SetBool("isRunning", true);

            transform.rotation = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, speedRotation * Time.deltaTime);
        }
        else
        {
            running = false;
            animator.SetBool("isRunning", false);
        }
    }

    void Gravity()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime * gravityScale;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

//            Jump();
        }
        else
        {
            characterController.stepOffset = 0;
        }
    }

    void Jump()
    {
        grounded = Physics.OverlapSphere(groundChecker.position, 0.3f, WhatsGround).Length > 0;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            // moveDirection.y = jumpForce;
            ySpeed = jumpForce;
        }

        if (!grounded)
        {
            jumping = true;
            animator.SetBool("isJumping", true);

        }
        else
        {
            jumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    public void Knockback()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        moveDirection.y = knockBackPower.y;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void IsKnocking()
    {
        if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = (playerModel.transform.forward * knockBackPower.x);
            moveDirection.y = yStore;

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            characterController.Move(moveDirection * Time.deltaTime);

            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;

            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }
        return velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundChecker.position, 0.3f);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.swordWoosh);
            animator.SetTrigger("attack");
            isAttacking = true;
            running = false;    
        }
    }

    void Defend()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !isAttacking)
        {
            animator.SetBool("defend", true);
            isDefend = true;
            Shield.SetActive(true);
        }
        else
        {
            isDefend = false;
            animator.SetBool("defend", false);
            Shield.SetActive(false);
        }
    }
}
