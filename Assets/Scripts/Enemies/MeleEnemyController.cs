using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemyController : MonoBehaviour
{
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private float wallCheckDistance;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float kockbackDuration;
    [SerializeField]
    private float lastTouchDamageTime;
    [SerializeField]
    private float tocuhDamageCooldown;
    [SerializeField]
    private float touchDamage;
    [SerializeField]
    private float touchDamageWidth;
    [SerializeField]
    private float touchDamageHeight;

    [SerializeField]
    private Vector2 knockbackSpeed;
    
    private float currentHealth;
    private float knockbackStartTime;
    public float experienceToGive;



    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform touchDamageCheck;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;

    [SerializeField]
    private bool seMueve;
    [SerializeField]
    private bool recibeAtaque;

    private bool groundDetected;
    private bool WallDetected;

    private int facingDirection;
    private int damageDirection;

    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    private Vector2 movement;
    private Vector2 touchDamageBotLeft;
    private Vector2 touchDamageTopRight;

    public Healthbar healthbar;


    private float[] attackDetails = new float[2];

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        aliveAnim = alive.GetComponent<Animator>();
        healthbar.SetHealth(currentHealth, maxHealth);

        facingDirection = 1;
    }


    private void Update()
    {
        switch(currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        WallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if(!groundDetected || WallDetected)
        {
            Flip();
        }
        else if(!seMueve || recibeAtaque)
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }

    private void ExitWalkingState()
    {

    }

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time >= knockbackStartTime * kockbackDuration)
        {
            SwtichState(State.Walking);
        }
    }

    private void ExitKnockbackState()
    {
        
        aliveAnim.SetBool("Knockback", false);
    }

    private void EnterDeadState()
    {
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    private void CheckTouchDamage()
    {
        if(Time.time >= lastTouchDamageTime + tocuhDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth/2), touchDamageCheck.position.y - (touchDamageHeight/2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth/2), touchDamageCheck.position.y + (touchDamageHeight/2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if(hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }

    private void Damage(float[] atackDetails)
    {
        currentHealth -= atackDetails[0];

        healthbar.SetHealth(currentHealth, maxHealth);


        if(atackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        if(currentHealth > 0.0f)
        {
            SwtichState(State.Knockback);
            recibeAtaque = true;
        }
        else if(currentHealth <= 0.0f)
        {
            ExperienceScript.instance.expModifier(experienceToGive);
            SwtichState(State.Dead);
        }

    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f,180.0f,0.0f);

    }


    private void SwtichState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x * wallCheckDistance, wallCheck.position.y));

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth/2), touchDamageCheck.position.y - (touchDamageHeight/2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth/2), touchDamageCheck.position.y - (touchDamageHeight/2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth/2), touchDamageCheck.position.y + (touchDamageHeight/2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth/2), touchDamageCheck.position.y + (touchDamageHeight/2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }
    
}
