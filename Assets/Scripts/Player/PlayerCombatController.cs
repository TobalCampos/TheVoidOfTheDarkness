using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCombatController : MonoBehaviour
{

    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    private float[] attackDetails = new float[2];

    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator anim;

    private PlayerSript PC;
    private PlayerStats PS;

    private GameControlPause gameControl;


    private void Start()
    {
      anim = GetComponent<Animator>();
      anim.SetBool("canAttack", combatEnabled); 
      PC = GetComponent<PlayerSript>(); 
      PS = GetComponent<PlayerStats>();
      gameControl = FindObjectOfType<GameControlPause>();
    }

    private void Update()
    {
        if (gameControl.isGameRunning())
        {
            CheckAttack(); 
        } 
    }

    public void CheckCombatInput(InputAction.CallbackContext context)
    {
    
            if(combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
    }

    public void CheckCombatInputTel()
    {
    
            if(combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
    }

    private void CheckAttack()
    {
        if(gotInput)
        {
            if(!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }

        }

        if(Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitBot()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position,attack1Radius, whatIsDamageable);

        attackDetails[0] = attack1Damage;
        attackDetails[1] = transform.position.x;

        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage",attackDetails);
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1",false);
    }

    private void Damage(float[] attackDetails)
    {
        if (!PC.GetDashStatus())
        {
            int direction;

            PS.DecreaseHealth(attackDetails[0]);

            if(attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            PC.Knockback(direction);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
    
}
