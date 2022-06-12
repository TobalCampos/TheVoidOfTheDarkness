using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform[] transforms;
    public float countdown;
    public bool BossActivado;

    public GameObject BossCompleto;
    public ApereceBoss AB;

    [SerializeField]
    private float maxHealth;
    public float currentHealth;
    public float experienceToGive;

    public HealthbarBoss healthbar;
    private GameObject Player;
    public GameObject Escudo;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballs;
    
    [SerializeField] private Transform firePoint2;
    [SerializeField] private GameObject fireballs2;

    [SerializeField]
    private LayerMask whatIsPlayer;

   
    private PlayerSript pc;
    private Animator anim;

    public float Timer;
    public float timerAtk1;
    public bool StopAttacks = false;

    private GameManager GM;

    // Start is called before the first frame update
   private void Start()
    {
        countdown = 10;
        Timer = 6;
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
        Player = GameObject.FindGameObjectWithTag("Player");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Awake()
    { 
        if(BossActivado)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(BossActivado)
        {
            countdown -= Time.deltaTime;
            Timer -= Time.deltaTime;
            CheckFaceDirection();
        }

        if(countdown < 0 && BossActivado)
        {
            countdown = 10;
            Teleport();
        }

        if(Timer < 0 && BossActivado)
        {
            Timer = 6;
            int NumeroRandom = Random.Range(1,3);
            if(NumeroRandom == 2)
            {
                Attack1();
            }
            else if(NumeroRandom == 1)
            {
                Attack2();
            }
        }

        if(StopAttacks)
        {
            timerAtk1 -= Time.deltaTime;
        }

            if(timerAtk1 <= 0)
            {
                anim.SetBool("Atk2", false);
                anim.SetBool("Atk1", false);
                StopAttacks = false;
                timerAtk1 = 1;
            }  

        CheckFase2();
        
    }

     public void Attack1()
    {
        fireballs.transform.position = firePoint.position;
        fireballs.GetComponent<MeteoritoBoss>().SetDirection();
        anim.SetBool("Atk1", true);  
        StopAttack();      
    }

    public void Attack2()
    {

        fireballs2.transform.position = firePoint2.position;
        Vector3 rotation = transform.eulerAngles;

        if (rotation.y == 180f)
        {
            fireballs2.GetComponent<BossMagia>().SetDirection(-1);
        }
        else
        {
            fireballs2.GetComponent<BossMagia>().SetDirection(1);
        }
            anim.SetBool("Atk2", true);  
            StopAttack();      
    }

    private void StopAttack()
    {
        StopAttacks = true;
    }

    public void CheckFase2()
    {
        if(currentHealth <= maxHealth/2)
        {
            Escudo.SetActive(true);
        }else
        {
            Escudo.SetActive(false);
        }
    }

    public void CheckFaceDirection()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > Player.transform.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;

    }

    public void Teleport()
    {
        var initialPosition = Random.Range(0,transforms.Length);
        transform.position = transforms[initialPosition].position; 
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
