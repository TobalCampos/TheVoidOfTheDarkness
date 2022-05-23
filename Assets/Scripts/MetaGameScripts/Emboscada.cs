using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emboscada : MonoBehaviour
{

    public GameObject Enemigo1;
    public GameObject Enemigo2;
    public GameObject Enemigo3;
    public GameObject Enemigo4;
    public GameObject Enemigo5;

    public GameObject Portal;

    private bool emboscadaON;

    // Start is called before the first frame update
    void Start()
    {
      emboscadaON = true;   
    }

    // Update is called once per frame
    void Update()
    {
      Emboscar();  
    }

    private void Emboscar()
    {
        if(Enemigo1 == null && Enemigo2 == null && Enemigo3 == null && Enemigo4 == null && Enemigo5 == null && emboscadaON)
        {
            Portal.SetActive(true);
            emboscadaON = false;
        }
    }
}
