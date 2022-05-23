using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
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
     private float attack1Damage, attack1Radius;
    [SerializeField]
    private LayerMask whatIsDamageable;
    [SerializeField]
    private Transform attack1HitBoxPos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {

        CheckAttackHitBox();

        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);


        lifetime += Time.deltaTime;
        if (lifetime > 1.5) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            hit = true;
            Deactivate();
        }

        
      
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position,attack1Radius, whatIsDamageable);

            attackDetails[0] = attack1Damage;
            attackDetails[1] = transform.position.x;

            foreach(Collider2D collider in detectedObjects)
            {
                collider.transform.parent.SendMessage("Damage",attackDetails);
                hit = true;
                Deactivate();
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
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
