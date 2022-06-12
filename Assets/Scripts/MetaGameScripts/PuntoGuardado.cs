using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoGuardado : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private GameObject RespawnPoint;
    public float positionx;
    public float positiony;

    public void Update()
    {
         player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
     
      if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("xPlayer",player.transform.position.x);
            PlayerPrefs.SetFloat("yPlayer",player.transform.position.y);
        }
    }

    public void ZonaRespawn()
    {
       float posx = PlayerPrefs.GetFloat("xPlayer",-25);
       float posy = PlayerPrefs.GetFloat("yPlayer",-3);
        RespawnPoint.transform.position = new Vector3(posx, posy, 0f);
    }
}
