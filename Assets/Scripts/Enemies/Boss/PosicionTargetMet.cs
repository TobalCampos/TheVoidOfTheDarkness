using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionTargetMet : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            CambiarPosicion();
        }
    }

    public void CambiarPosicion()
    {
        float newPos = Player.transform.position.x;
        transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
    }
}
