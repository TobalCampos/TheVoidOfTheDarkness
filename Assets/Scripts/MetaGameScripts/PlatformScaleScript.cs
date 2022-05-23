using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScaleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(gameObject.GetComponent<ObjectMovement>().willDestroy)
            {
                gameObject.GetComponent<ObjectMovement>().startCd = true;
            }

            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
             if(gameObject.GetComponent<ObjectMovement>().willDestroy)
            {
                gameObject.GetComponent<ObjectMovement>().startCd = false;
                gameObject.GetComponent<ObjectMovement>().destroyCd = gameObject.GetComponent<ObjectMovement>().timeToDestroy;
            }

            collision.transform.SetParent(null);
        }
    }
}
