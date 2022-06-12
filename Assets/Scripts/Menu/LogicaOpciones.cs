using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LogicaOpciones : MonoBehaviour
{
    public ControladorOpciones panelOpciones;
    public GameControlPause GameControlPause;
    public GameObject menu;
    public GameObject btnatras;
    private bool numeroOpciones = true;

    public PlayerSript Ps;
    public PlayerCombatController Pc;
    public PlayerHab Ph;
    public Potions Pt;
    // Start is called before the first frame update
    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("opciones").GetComponent<ControladorOpciones>();
        Ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSript>();
        Pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        Ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHab>();
        Pt = GameObject.FindGameObjectWithTag("Player").GetComponent<Potions>();

    }

    public void SaltoPlayer(InputAction.CallbackContext context)
    {
        Ps.JumpInput(context);
    }

     public void DashPlayer(InputAction.CallbackContext context)
    {
        Ps.DashInput(context);
    }

     public void AtkPlayer(InputAction.CallbackContext context)
    {
        Pc.CheckCombatInput(context);
    }

    public void HabPlayer(InputAction.CallbackContext context)
    {
        Ph.Habilidad(context);
    }

    public void MovPlayer(InputAction.CallbackContext context)
    {
        Ps.MoveInput(context);
    }
    
     public void DialogosPlayer(InputAction.CallbackContext context)
    {
        Ps.Dialogos(context);
    }

    public void PotionPlayer(InputAction.CallbackContext context)
    {
        Pt.Curarse(context);
    }

    public void DetectarPersonaje()
    {
        Ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSript>();
        Pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();
        Ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHab>();
        Pt = GameObject.FindGameObjectWithTag("Player").GetComponent<Potions>();
    }


    // Update is called once per frame
    public void EscapeMenu(InputAction.CallbackContext context)
    {
        if(numeroOpciones == true)
        {
            AbrirMenu();
            numeroOpciones = false;
            GameControlPause = GameObject.FindGameObjectWithTag("Control").GetComponent<GameControlPause>();
            GameControlPause.ChangeGameRunningState();
        }else if(numeroOpciones == false )
        {
            ReanudarJuego();
            numeroOpciones = true;
            GameControlPause = GameObject.FindGameObjectWithTag("Control").GetComponent<GameControlPause>();
            GameControlPause.ChangeGameRunningState();
        } 
    }

    public void EscapeMenuTel()
    {
        if(numeroOpciones == true)
        {
            AbrirMenu();
            numeroOpciones = false;
            GameControlPause = GameObject.FindGameObjectWithTag("Control").GetComponent<GameControlPause>();
            GameControlPause.ChangeGameRunningState();
        }else if(numeroOpciones == false )
        {
            ReanudarJuego();
            numeroOpciones = true;
            GameControlPause = GameObject.FindGameObjectWithTag("Control").GetComponent<GameControlPause>();
            GameControlPause.ChangeGameRunningState();
        } 
    }
    
    public void AbrirMenu()
    {
        menu.SetActive(true);
        btnatras.SetActive(false);
    }

    public void ReanudarJuego()
    {
        menu.SetActive(false);
        panelOpciones.pantallaOpciones.SetActive(false);
        btnatras.SetActive(false);
        numeroOpciones = true;
    }

    public void MenuOpciones()
    {
        menu.SetActive(false);
        panelOpciones.pantallaOpciones.SetActive(true);
        btnatras.SetActive(true);
    }

    public void Salir()
    {
    GameControlPause.ChangeGameRunningState();
     SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void IrAtras()
    {
        menu.SetActive(true);
        panelOpciones.pantallaOpciones.SetActive(false);
        btnatras.SetActive(false); 
    }

    

    public void CambiarNumeroOpciones()
    {
         numeroOpciones = !numeroOpciones;
    }
}
