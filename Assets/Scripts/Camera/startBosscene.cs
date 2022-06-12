using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class startBosscene : MonoBehaviour
{
    public Animator camAnim;
    public GameObject Activar;

    
    private CinemachineVirtualCamera CVC2;
    private GameObject Player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            camAnim = GameObject.Find("cameraManager").GetComponent<Animator>();
            CVC2 = GameObject.Find("CM vcam5").GetComponent<CinemachineVirtualCamera>();
            Player = GameObject.FindWithTag("Player");
            CVC2.m_Follow = Player.transform;
            camAnim.SetBool("zona", true);
            Invoke(nameof(GoTo2), 3f);
            Invoke(nameof(GoToNewMain), 6f);              
        }
    }

    void GoTo2()
    {
        camAnim.SetBool("boss", true);
    }

    void GoToNewMain()
    {
        camAnim.SetBool("MainCamBoss", true);
        Activar.SetActive(true);
        Destroy(gameObject);
    }
    
}

