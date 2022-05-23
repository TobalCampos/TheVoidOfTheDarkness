using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleEnemy : MonoBehaviour
{

    [SerializeField]
    private float maxHealth;  
    public float experienceToGive;


    private GameObject aliveGO;
    private Rigidbody2D rbAlive;
    private Animator aliveAnim;

    private float currentHealth;

    public float moveSpeed;
    public GameObject[] wayPoints;

    int nextWaypoint = 1;
    float distToPoint;

    private float[] attackDetails = new float[2];
    
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
    private Transform touchDamageCheck;

    [SerializeField]
    private LayerMask whatIsPlayer;

    private Vector2 touchDamageBotLeft;
    private Vector2 touchDamageTopRight;


    private PlayerSript pc;
    public Healthbar healthbar;


    void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerSript>();
        aliveGO = transform.Find("Alive").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        healthbar.SetHealth(currentHealth, maxHealth);

        aliveGO.SetActive(true);
        
    }

    void Update()
    {
        Move();   
        CheckTouchDamage();
    }

    void Move()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoints[nextWaypoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWaypoint].transform.position, moveSpeed * Time.deltaTime);

        if(distToPoint < 0.2f)
        {
            TakeTurn();
        }
    }

    void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoints[nextWaypoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        ChooseNextWaypoint();
    }

    void ChooseNextWaypoint()
    {
        nextWaypoint++;

        if(nextWaypoint == wayPoints.Length)
        {
            nextWaypoint = 0;
        }
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
                hit.SendMessage("Damage", attackDetails);
            }
        }
    
    }
     private void OnDrawGizmos()
    {

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

