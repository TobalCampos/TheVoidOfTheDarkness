using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField]
    private bool apllyKnockback;

    private float currentHealth, knockbackStart;

    private int playerFacingDirection;
    private bool  knockback;

    private PlayerSript pc;
    private GameObject aliveGO;
    private Rigidbody2D rbAlive;
    private Animator aliveAnim;

    public Healthbar healthbar;

    private void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerSript>();
        aliveGO = transform.Find("Alive").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);

        healthbar.SetHealth(currentHealth, maxHealth);

    }

    private void Update()
    {
        CheckKnockback();
    }

    private void Damage(float[] Details)
    {
        currentHealth -= Details[0];

        if(Details[1] < aliveGO.transform.position.x)
        {
            playerFacingDirection = 1;
        }
        else
        {
            playerFacingDirection = 1;
        }

        healthbar.SetHealth(currentHealth, maxHealth);
      

        if(apllyKnockback && currentHealth > 0.0f)
        {
            Knockback();
        }

        if(currentHealth < 0.0f)
        {
            Die();
        }

    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if(Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);

    }
}
