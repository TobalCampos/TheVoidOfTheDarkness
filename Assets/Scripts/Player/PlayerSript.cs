using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSript : MonoBehaviour
{
    private NPC_Controller npc;

    private float movementInputDirection;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;
    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;
    private float knockbackStartTime;
    [SerializeField]
    private float knockbackDuration;

    [SerializeField]
    private Vector2 knockbackSpeed;

    private Rigidbody2D rb;
    private Animator anim;

    private int amountOfJumpsLeft;
    public int facingDirection = 1;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultipler = 0.95f;
    public float variableJumpMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;
    public float JumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;
    public float wallJumpTimerSet = 0.5f;

    public int amountOfJumps = 2;
    private int lastWallJumpDirection;

    private bool isFacingRight = true;
    private bool isWalking ;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isWallSliding;
    private bool isDashing = false;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;
    private bool knockback;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;

    public LayerMask whatIsGround;

    private GameControlPause gameControl;
    private PuntoGuardado PuntoGuardado;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameControl = FindObjectOfType<GameControlPause>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();

        transform.position = new Vector3(PlayerPrefs.GetFloat("xPlayer",-25), PlayerPrefs.GetFloat("yPlayer",-3), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inDialogue() && gameControl.isGameRunning())
        {
            CheckInput();
            CheckMovementDirection();
            UpdateAnimations();
            CheckIfCanJump();
            CheckIfWallSliding();
            CheckDash();
            CheckJump();
            CheckKnockback();
        } 
    }
    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
        
    }

    public bool GetDashStatus()
    {
        return isDashing;
    }

    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    private void CheckIfWallSliding()
    {
        if(isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }else
        {
            isWallSliding = false;
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if(isTouchingWall)
        {
            canWallJump = true;
            amountOfJumpsLeft = amountOfJumps;
        }
        
        if(amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    private void CheckMovementDirection()
    {   
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if(Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }else 
        {
            isWalking = false;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
           if(isGrounded || (amountOfJumpsLeft > 0 && isTouchingWall))
           {
               NormalJump();
           }
           else
           {
               jumpTimer = JumpTimerSet;
               isAttemptingToJump = true;
           }
        }
        
        if(Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if(!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if(!canMove)
        {
            turnTimer -= Time.deltaTime;
            if(turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }



        if(checkJumpMultiplier && !Input.GetButtonUp("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpMultiplier);
        }

        if (Input.GetButtonDown("Dash"))
        {
            if(Time.time >= (lastDash + dashCoolDown))
            AttemptToDash();
        }
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }

    private void CheckDash()
    {
        if(isDashing)
        {
            if(dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
                dashTimeLeft -= Time.deltaTime;

                if(Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }
        }
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void CheckJump()
    {
       if(jumpTimer > 0)
       {
           if(!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection == -facingDirection)
           {
               WallJump();
           }
           else if (isGrounded)
           {
               NormalJump();
           }
       }

       if(isAttemptingToJump)
       {
           jumpTimer -= Time.deltaTime;
       }

       if(wallJumpTimer > 0)
       {
           if(hasWallJumped && movementInputDirection == -lastWallJumpDirection)
           {
               rb.velocity = new Vector2(rb.velocity.x, 0.0f);
               hasWallJumped = false;
           }else if(wallJumpTimer <= 0)
           {
               hasWallJumped = false;
           }else{
               wallJumpTimer -= Time.deltaTime;
           }
       }

       if(isAttemptingToJump)
       {
           jumpTimer -= Time.deltaTime;
       }
 
    }

    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {
        if(canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    }

    private void ApplyMovement()
    {

        if(!isGrounded && !isWallSliding && movementInputDirection == 0 && !knockback)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultipler, rb.velocity.y);
        }else if (canMove && !knockback)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    public void DisableFlip()
    {
        canFlip = false;
    }

     public void EnableFlip()
    {
        canFlip = true;
    }

    private void Flip()
    {
        if (!isWallSliding && canFlip && !knockback)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f,180.0f,0.0f);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    private bool inDialogue()
    {
        if(npc !=null )
            return npc.DialogueActive();
        else
            return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            npc =collision.gameObject.GetComponent<NPC_Controller>();

            if (Input.GetKey(KeyCode.E))
                npc.ActivateDialogue();
        }
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
    }

}
