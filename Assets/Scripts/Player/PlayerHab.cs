using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHab : MonoBehaviour
{
   [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballs;

    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;

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

        if (Input.GetMouseButton(1) && cooldownTimer > attackCooldown)
            Attack();
        cooldownTimer += Time.deltaTime;
        playerHab.fillAmount = cooldownTimer/attackCooldown;
        float TimeInt = 25 - Mathf.Round(cooldownTimer);

        if (TimeInt > 0)
        {
         textTime.text = TimeInt.ToString();
        }
        else{
            textTime.text = "";
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attackhab");
        cooldownTimer = 0;

        fireballs.transform.position = firePoint.position;
        fireballs.GetComponent<Proyectil>().SetDirection(PS.facingDirection);
    }
 
}
