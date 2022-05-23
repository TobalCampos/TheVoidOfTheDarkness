using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed;
    public float timeToWait;
    public bool shouldMove;
    public bool shouldWait;
    public bool canContinue;
    public bool willDestroy;
    public bool startCd;
    public float timeToDestroy;
    public float destroyCd;
    bool moveToA;
    bool moveToB;
    public GameObject plataforma1;
    public GameObject plataforma2;
    public GameObject plataforma3;


    private void Start()
    {
        moveToA = true;
        moveToB = false;
        canContinue = true;
        destroyCd = timeToDestroy;
    }

    private void Update()
    {
       if(shouldMove)
       {
           MoveObject();
       }

       if(startCd)
       {
           destroyCd -= Time.deltaTime;
           if(destroyCd <= 0)
           {
               StartCoroutine(ReactivatePlatform());
               destroyCd = timeToDestroy;
               startCd = false;
           }
       }  
    }

    IEnumerator ReactivatePlatform()
    {
        bool reactivate = true;
        plataforma1.SetActive(false);
        plataforma2.SetActive(false);
        plataforma3.SetActive(false);
        BoxCollider2D myColliders = gameObject.GetComponent<BoxCollider2D>();
        myColliders.enabled = false;


        yield return new WaitForSeconds(2f);

        plataforma1.SetActive(true);
        plataforma2.SetActive(true);
        plataforma3.SetActive(true);
        myColliders.enabled = true;
        reactivate = false;
    }

    private void MoveObject()
    {
        float distanceToA = Vector2.Distance(transform.position, pointA.position);
        float distanceToB= Vector2.Distance(transform.position, pointB.position);

        if(distanceToA > 0.1f && moveToA)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed *Time.deltaTime);
            if(distanceToA < 0.3f && canContinue)
            {
                if (shouldWait)
                {
                    StartCoroutine(Waiter());
                    moveToA = false;
                    moveToB = true;
                }
                else
                {
                    moveToA = false;
                    moveToB = true;
                }
            }
        }

        if(distanceToB > 0.1f && moveToB)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed *Time.deltaTime);
            if(distanceToB < 0.3f && canContinue)
            {
                if (shouldWait)
                {
                    StartCoroutine(Waiter());
                    moveToA = true;
                    moveToB = false;
                }
                else
                {
                    moveToA = true;
                    moveToB = false;
                }
            }
        }
    }

    IEnumerator Waiter()
    {
        shouldMove = false;
        canContinue = false;
        yield return new WaitForSeconds(timeToWait);
        shouldMove = true;
        canContinue = true;
    }
}
