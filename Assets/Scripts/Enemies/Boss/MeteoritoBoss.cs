using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritoBoss : MonoBehaviour
{
     [SerializeField] private float speed;
    public int direction;
    private bool hit;
    private float lifetime;

    private PlayerSript PS;
    private Animator anim;
    private BoxCollider2D boxCollider;

     private float[] attackDetails = new float[2];

    [SerializeField]
    private float lastTouchDamageTime;
    [SerializeField]
    private float tocuhDamageCooldown;
    [SerializeField]
    private float touchDamage;

    
    [SerializeField]
    private Transform touchDamageCheck;

    [SerializeField]
    private LayerMask whatIsPlayer;

    private Vector2 touchDamageBotLeft;
    private Vector2 touchDamageTopRight;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
         if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);


        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    
     public void SetDirection()
    {
        lifetime = 0;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            hit = true;
            Deactivate();
        }

         if(collision.gameObject.tag == "Player")
        {        

            if(collision != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = transform.position.x;
                collision.SendMessage("Damage", attackDetails);
            }

        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
