using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaOpciones : MonoBehaviour
{
    public ControladorOpciones panelOpciones;
    public GameControlPause GameControlPause;
    public GameObject menu;
    public GameObject btnatras;
    private bool numeroOpciones = true;
    // Start is called before the first frame update
    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("opciones").GetComponent<ControladorOpciones>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && numeroOpciones == true)
        {
            AbrirMenu();
            numeroOpciones = false;
            GameControlPause = GameObject.FindGameObjectWithTag("Control").GetComponent<GameControlPause>();
            GameControlPause.ChangeGameRunningState();
        }else if(Input.GetKeyDown(KeyCode.Escape) && numeroOpciones == false )
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
