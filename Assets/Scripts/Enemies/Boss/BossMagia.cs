using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMagia : MonoBehaviour
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
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {

        CheckTouchDamage();

        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);


        lifetime += Time.deltaTime;
        if (lifetime > 2.5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            hit = true;
            Deactivate();
        }

        if(collision.CompareTag("Player"))
        {
            
        }
    }

   private void CheckTouchDamage()
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
                Deactivate();
            }
        }
    
    }

    
   public void SetDirection(int _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth/2), touchDamageCheck.position.y - (touchDamageHeight/2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth/2), touchDamageCheck.position.y - (touchDamageHeight/2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth/2), touchDamageCheck.position.y + (touchDamageHeight/2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth/2), touchDamageCheck.position.y + (touchDamageHeight/2));
    }
}
