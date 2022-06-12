using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LevelActivation : MonoBehaviour
{
    public GameObject lever;
    public GameObject objectToActivate;
    private bool Colisionando;

    [SerializeField]
    private AudioClip SoundEffect;

    public PlayerInputTel Pt;
    public string stringName;

    void Update()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputTel>();
           Pt.CambiarPalanca(stringName);
           Colisionando = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       Colisionando = false;
    }

    public void Activar(InputAction.CallbackContext context)
    {
        if(Colisionando)
        {
            objectToActivate.GetComponent<ObjectMovement>().shouldMove = true;
            soundManager.instance.PlaySound(SoundEffect);
        }
    }

    public void ActivarTel()
    {
        if(Colisionando)
        {
            objectToActivate.GetComponent<ObjectMovement>().shouldMove = true;
            soundManager.instance.PlaySound(SoundEffect);
        }
    }
}
