using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHab : MonoBehaviour
{
   [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballs;

    private Animator anim;
    public float cooldownTimer = Mathf.Infinity;

    private PlayerSript PS;
    public Image playerHab;
    public Text textTime;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        PS = GetComponent<PlayerSript>(); 
        GameObject Holder = GameObject.Find("FireballHolder");
        fireballs = Holder.transform.GetChild(0).gameObject;

    }

    private void Update()
    {   
        
            playerHab.fillAmount = cooldownTimer/attackCooldown;
            cooldownTimer += Time.deltaTime;
            float TimeInt = 25 - Mathf.Round(cooldownTimer);

             if (TimeInt > 0)
            {
            textTime.text = TimeInt.ToString();
            }
            else{
                textTime.text = "";
            }
            
    }

    public void Habilidad(InputAction.CallbackContext context)
    {
        if(cooldownTimer > attackCooldown)
        {
            Attack();
        }
    }

    public void HabilidadTel()
    {
        if(cooldownTimer > attackCooldown)
        {
            Attack();
        }
    }

    public void FinishHab()
    {
        anim.SetBool("attackhab",false);
    }
    

    private void Attack()
    {
        anim.SetBool("attackhab",true);
        cooldownTimer = 0;

        fireballs.transform.position = firePoint.position;
        fireballs.GetComponent<Proyectil>().SetDirection(PS.facingDirection);
    }
 
}
