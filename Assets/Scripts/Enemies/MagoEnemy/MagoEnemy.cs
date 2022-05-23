using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoEnemy : MonoBehaviour
{
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    
    private float distance;
    private bool attackMode;
    private float intTimer;
    private float currentHealth;
    public float experienceToGive;


    [SerializeField]
    private float maxHealth;

     
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballs;
    
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField]
    private LayerMask whatIsPlayer;

   
    private PlayerSript pc;
    public Healthbar healthbar;
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
    }

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerSript>(); 
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       
        if(!attackMode)
        {
            Move();
        }

        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Atack"))
        {
            SelectTarget();
        }
 

        if (inRange)
        {
            EnemyLogic();
        }

        cooldownTimer += Time.deltaTime;
    }


    void EnemyLogic()
    {

        if(distance > attackDistance)
        {
            StopAttack();
        }
        else if(attackDistance >= distance)
        {
            Attack();
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Atack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void Attack()
    {
        attackMode = true;

        if (cooldownTimer > attackCooldown)
        {
            cooldownTimer = 0;

            fireballs.transform.position = firePoint.position;
            Vector3 rotation = transform.eulerAngles;

            if (rotation.y == 180f)
            {
                fireballs.GetComponent<MagoMagia>().SetDirection(-1);
            }
            else
            {
                fireballs.GetComponent<MagoMagia>().SetDirection(1);
            }
            anim.SetBool("canWalk", false);
            anim.SetBool("Attack", true);
        } 
            timer -= Time.deltaTime;

            if(timer <= 0 && attackMode)
            {
                anim.SetBool("Attack", false);
                timer = intTimer;
            }        
    }

    void StopAttack()
    {
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    
    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
            attackMode = false;
        }
        else
        {
            target = rightLimit;
            attackMode = false;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation; 
    }

    private void Damage(float[] Details)
    {
        currentHealth -= Details[0];
        healthbar.SetHealth(currentHealth, maxHealth);

        if(currentHealth < 0.0f)
        {
            Die();
        }

    }

    private void Die()
    {
        ExperienceScript.instance.expModifier(experienceToGive);
        Destroy(gameObject);
    }

}
