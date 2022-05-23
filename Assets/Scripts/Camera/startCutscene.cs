using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startCutscene : MonoBehaviour
{
    public Animator camAnim;
    public static bool isCustsceneOn;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isCustsceneOn = true;
            camAnim.SetBool("custcene1", true);
            Invoke(nameof(GoTo2), 3f);
            Invoke(nameof(StopCutscene), 6f);
        }
    }

    void GoTo2()
    {
        camAnim.SetBool("goto2", true);
        camAnim.SetBool("custcene1", false); 
    }

    void StopCutscene()
    {
        isCustsceneOn = false;
        camAnim.SetBool("goto2", false);
        Destroy(gameObject);
    }
}
