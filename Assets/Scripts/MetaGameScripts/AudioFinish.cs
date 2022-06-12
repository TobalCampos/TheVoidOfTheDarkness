using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioFinish : MonoBehaviour
{
    [SerializeField]
    private AudioClip Audio1;
    public string CargarEscena;

    public float delay = 0;
    public float delayAplicar = 41;

    // Start is called before the first frame update
    void Start()
    {
       soundManager.instance.PlaySound(Audio1);

       PlayerPrefs.SetString("CargarEscena","Lvl1");    
       CargarEscena = PlayerPrefs.GetString("CargarEscena","Audio1");
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;

        if(delay >= delayAplicar)
        {
             PlayerPrefs.SetFloat("xPlayer",-25);
            PlayerPrefs.SetFloat("yPlayer",-3);
            SceneManager.LoadScene(CargarEscena, LoadSceneMode.Single);
        }
    }
}
