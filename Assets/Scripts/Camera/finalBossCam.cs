using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBossCam : MonoBehaviour
{
   public Animator camAnim;
   public GameObject Boss;

    // Update is called once per frame
    void Update()
    {
        if(Boss == null)
        {
            camAnim = GameObject.Find("cameraManager").GetComponent<Animator>();
            StopCutscene();
        }
        
    }

    public void StopCutscene()
    {
        camAnim.SetBool("boss", false);
        camAnim.SetBool("zona", false); 
        camAnim.SetBool("MainCamBoss", false);    
         Destroy(gameObject);
    }
}
