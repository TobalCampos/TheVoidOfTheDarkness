using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isOnTouch : MonoBehaviour
{
    public GameObject ControlesTel;

    // Start is called before the first frame update
    void Start()
    {
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            ControlesTel.SetActive(true);

        }
        else
        {
             ControlesTel.SetActive(false);        
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            ControlesTel.SetActive(true);
        }
        else
        {
             ControlesTel.SetActive(false);        
        }      
    }
}
