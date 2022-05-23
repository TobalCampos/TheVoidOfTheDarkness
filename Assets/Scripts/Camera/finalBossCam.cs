using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBossCam : MonoBehaviour
{
   public Animator camAnim;
   public GameObject FinalBoss;

    // Update is called once per frame
    void Update()
    {
        if(FinalBoss == null)
        {
            camAnim.SetBool("MainCamBoss", false);      
            Destroy(gameObject);
        }
        
    }
}
