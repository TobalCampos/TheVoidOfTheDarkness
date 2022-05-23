using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlPause : MonoBehaviour
{
    [SerializeField]
    private bool gameRunning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameRunningState()
    {
        gameRunning = !gameRunning;

        if(gameRunning)
        {
            Debug.Log("Game Running");
            Time.timeScale = 1f;

            AudioSource[] audios = FindObjectsOfType<AudioSource>();

            foreach(AudioSource a in audios)
            {
                a.Play();
            }
        }
        else
        {
            Debug.Log("Game Paused");
            Time.timeScale = 0f;

             AudioSource[] audios = FindObjectsOfType<AudioSource>();

            foreach(AudioSource a in audios)
            {
                a.Pause();
            }

        }
    }

    public bool isGameRunning()
    {
        return gameRunning;
    }
}
