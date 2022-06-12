using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class MainMenuNew : MonoBehaviour
{
 
    Animator CameraObject;

    [Header("Loaded Scene")]
    [Tooltip("The name of the scene")]
    public string sceneName="";
 
 
    [Header("Panels")]
    [Tooltip("Panel Main")]
    public GameObject MainPanel;
    [Tooltip("The UI Pop-Up when 'EXIT' is clicked")]
    public GameObject PanelareYouSure;
    [Tooltip("Panel de options")]
    public GameObject PanelJugar;
    
    
 
    [Header("SFX")]
    [Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
    public GameObject hoverSound;
    [Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
    public GameObject sliderSound;
    [Tooltip("The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
    public GameObject swooshSound;
 
    [Header("MenuJugar")]
    public GameObject nuevaPartida;
    public GameObject continuar;
    public GameObject atrasjugar;

    //Menu inicial
    [Tooltip("Jugar Button GameObject Pop Up")]
    public GameObject jugarBtn;
    [Tooltip("Opciones Button GameObject Pop Up")]
    public GameObject opcionesBtn;
    [Tooltip("Salir Button GameObject Pop Up")]
    public GameObject salirBtn;


 
    //pantalla are you sure
    public GameObject noBtn;
    public GameObject Atrasoptions;
    public ControladorOpciones panelOpciones;
    public string CargarEscena;


    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("opciones").GetComponent<ControladorOpciones>();
        CargarEscena = PlayerPrefs.GetString("CargarEscena","Audio1");
    }

    public void Jugar()
    {
        PanelJugar.gameObject.SetActive(true);
        MainPanel.gameObject.SetActive(false);
    }

    public void AtrasJugar()
    {
        PanelJugar.gameObject.SetActive(false);
        MainPanel.gameObject.SetActive(true);  
    }
 
    public void NewGame()
    {      
        PlayerPrefs.DeleteAll();
        CargarEscena = PlayerPrefs.GetString("CargarEscena","Audio1");    
        SceneManager.LoadScene(CargarEscena, LoadSceneMode.Single);
    }

    public void ContinuarGame()
    {
        SceneManager.LoadScene(CargarEscena, LoadSceneMode.Single);
    }

     public void AtrasOptions()
    {
        panelOpciones.pantallaOpciones.SetActive(false);   
        Atrasoptions.SetActive(false);    
        MainPanel.gameObject.SetActive(true);  
        jugarBtn.GetComponent<Button>().Select(); 
         
    }

     public void OpenOptions()
    {
        panelOpciones.pantallaOpciones.SetActive(true);
        Atrasoptions.SetActive(true);
        MainPanel.gameObject.SetActive(false);  
    }
 
 
    public void PlayHover()
    {
        hoverSound.GetComponent<AudioSource>().Play();
    }
 
    public void PlaySFXHover()
    {
        sliderSound.GetComponent<AudioSource>().Play();
    }
 
    public void PlaySwoosh()
    {
        swooshSound.GetComponent<AudioSource>().Play();
    }
 
    // Are You Sure - Quit Panel Pop Up
    public void AreYouSure()
    {
        PanelareYouSure.gameObject.SetActive(true);
        noBtn.GetComponent<Button>().Select();
        jugarBtn.gameObject.SetActive(false);
        opcionesBtn.gameObject.SetActive(false);
 
        salirBtn.gameObject.SetActive(false);         
    }
 
    public void No()
    {
        PanelareYouSure.gameObject.SetActive(false);
        jugarBtn.gameObject.SetActive(true);
        opcionesBtn.gameObject.SetActive(true);
 
        salirBtn.gameObject.SetActive(true);      
        jugarBtn.GetComponent<Button>().Select();  
    }
 
    public void Yes()
    {
        Application.Quit();
    }
}

