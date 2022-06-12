using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerActivator : MonoBehaviour
{
    public GameObject ObjetoActivar;

    [SerializeField]
    private AudioClip SoundEffect;

    public PlayerInputTel Pt;

    void Start()
    {
        Pt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputTel>();
    }

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.CompareTag("Player"))
        {
            Pt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputTel>();
            ObjetoActivar.SetActive(true);
            Pt.ActivarSaltoDeNivel();
            soundManager.instance.PlaySound(SoundEffect);
        }
    } 
}
