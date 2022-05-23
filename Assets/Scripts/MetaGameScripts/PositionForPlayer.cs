using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionForPlayer : MonoBehaviour
{
    public GameObject player;
    public float positionx;
    public float positiony;

    public void Update()
    {
         player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.CompareTag("Player"))
        {
            player.transform.position = new Vector3(positionx,positiony,0);
        }
    }
}
