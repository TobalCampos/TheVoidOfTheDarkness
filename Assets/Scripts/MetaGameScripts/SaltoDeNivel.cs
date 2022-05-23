using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaltoDeNivel : MonoBehaviour
{
    public string levelName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }
}
