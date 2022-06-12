using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossActivator : MonoBehaviour
{

    [SerializeField]
    private AudioClip BossSound;

    [SerializeField]
    private AudioClip victory;

    [SerializeField]
    private GameObject CanvasHpBoss;

    [SerializeField]
    private GameObject BossVivo;

    public Boss ActivarBoss;

    private bool zonaBoss = false;

    public float delay = 0;
    public float delayAplicar = 6;

    public PlayerStats Ps;
    public ApereceBoss Ab;


    // Start is called before the first frame update
    void Start()
    {
        Ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Ab = GameObject.Find("RespawnBoss").GetComponent<ApereceBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BossVivo && zonaBoss)
        {
            soundManager.instance.PlaySound(BossSound);
            CanvasHpBoss.SetActive(true);
        }

        if(!BossVivo && Ps.currentHealth > 0)
        {
             delay += Time.deltaTime;
             
            if(delay >= delayAplicar)
            {
                SceneManager.LoadScene("Creditos", LoadSceneMode.Single);
            }
        }

        if(!BossVivo && zonaBoss)
        {
            QuitarBarraVida();
            Ab.RestartBoss();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
      if (collision.CompareTag("Player"))
      {
        zonaBoss = true;
        ActivarBoss.BossActivado = true;

      }
        
    }

    public void QuitarBarraVida()
    {
        zonaBoss = false;
        CanvasHpBoss.SetActive(false);
        soundManager.instance.StopSound();
        soundManager.instance.PlaySound(victory);
    }

}
