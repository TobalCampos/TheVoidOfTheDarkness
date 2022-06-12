using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscudoDMG : MonoBehaviour
{

    private PlayerSript PS;
    private CapsuleCollider2D boxCollider;

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


    private void Awake()
    {
        boxCollider = GetComponent<CapsuleCollider2D>();
    }
      


     private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Time.time >= lastTouchDamageTime + tocuhDamageCooldown)
        {
              touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth/2), touchDamageCheck.position.y - (touchDamageHeight/2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth/2), touchDamageCheck.position.y + (touchDamageHeight/2));

            Collider2D golpe = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if(golpe != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = transform.position.x;
                golpe.SendMessage("Damage", attackDetails);
            }
        }

        }
    }
}
