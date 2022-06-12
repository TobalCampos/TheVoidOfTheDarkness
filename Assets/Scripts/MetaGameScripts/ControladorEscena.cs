using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEscena : MonoBehaviour
{
    public string CargarEscena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
     
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetString("CargarEscena",CargarEscena);
        }
    } 
}
