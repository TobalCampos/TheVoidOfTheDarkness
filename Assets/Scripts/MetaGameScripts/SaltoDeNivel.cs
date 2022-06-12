using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SaltoDeNivel : MonoBehaviour
{
    public string levelName;
    private bool Colisionando = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           Colisionando = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       Colisionando = false;
    }

    public void SaltoNivel(InputAction.CallbackContext context)
    {
        if(Colisionando)
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            PlayerPrefs.SetFloat("xPlayer",-25);
            PlayerPrefs.SetFloat("yPlayer",-3);
        }
    }

     public void SaltoNivelTel()
    {
        if(Colisionando)
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            PlayerPrefs.SetFloat("xPlayer",-25);
            PlayerPrefs.SetFloat("yPlayer",-3);
        }
    }
}
