using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startBosscene : MonoBehaviour
{
    public Animator camAnim;
    public GameObject FinalBoss;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            camAnim.SetBool("zona", true);
            Invoke(nameof(GoTo2), 3f);
            Invoke(nameof(StopCutscene), 6f); 
            
        }
    }

    void GoTo2()
    {
        camAnim.SetBool("boss", true);
        camAnim.SetBool("zona", false); 
    }

    void StopCutscene()
    {
        camAnim.SetBool("boss", false);
        camAnim.SetBool("MainCamBoss", true);
         FinalBoss.SetActive(true);
        Destroy(gameObject);
    }
}

