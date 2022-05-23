using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerActivator : MonoBehaviour
{
    public GameObject ObjetoActivar;

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.CompareTag("Player"))
        {
            ObjetoActivar.SetActive(true);
        }
    } 
}
