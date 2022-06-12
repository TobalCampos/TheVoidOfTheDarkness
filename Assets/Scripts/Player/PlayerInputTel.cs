using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class PlayerInputTel : MonoBehaviour
{
    public LogicaOpciones LO;
    public SaltoDeNivel SDN;
    public LevelActivation LA;
    public DialogueHolder DH;

    // Start is called before the first frame update
    void Start()
    {
        LO = GameObject.Find("ControladorDeEscena").GetComponent<LogicaOpciones>();
        SDN = GameObject.Find("espejocristiloSaltoNivel").GetComponent<SaltoDeNivel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarSaltoDeNivel()
    {
        SDN = GameObject.Find("espejocristiloSaltoNivel").GetComponent<SaltoDeNivel>();
    }

    public void CambiarPalanca(string Palanca)
    {
        LA = GameObject.Find(Palanca).GetComponent<LevelActivation>();
    }

    public void LevelActivar()
    {
        if(LA != null)
        LA.ActivarTel();
    }

    public void SaltoDeNivel()
    {
        SDN.SaltoNivelTel();
    }

    public void AbrirMenu()
    {
        LO.EscapeMenuTel();

        DH = GameObject.FindGameObjectWithTag("DialogueHolder").GetComponent<DialogueHolder>();
        DH.CerrarDialogoTel();
    }

    
}
