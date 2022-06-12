using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioFinalJuego : MonoBehaviour
{
    [SerializeField]
    private AudioClip Audio1;
    [SerializeField]
    private AudioClip Musica;
    public Animator camAnim;


    public float delay = 0;
    public float delayAplicar;

    // Start is called before the first frame update
    void Start()
    {
        soundManager.instance.PlaySound(Audio1);
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;

        if(delay >= delayAplicar)
        {
            camAnim.SetBool("TrueCredits", true);
            soundManager.instance.PlaySound(Musica);
        }

        if(delay > 120)
        {
            soundManager.instance.StopSound();
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
