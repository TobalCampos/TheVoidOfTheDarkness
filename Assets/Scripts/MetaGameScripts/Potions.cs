using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potions : MonoBehaviour
{
    [SerializeField] private int pocionVida;
    private PlayerStats PS;

    
    public Image potionImg;
    public Text textCant;
    public Sprite potionNull;
    public Sprite potionMitad;

    void Start()
    {
        PS = GetComponent<PlayerStats>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            UsarPocionVida();
        }

        textCant.text = "x"+ pocionVida.ToString();

        if(pocionVida <= 3 && pocionVida > 0)
        {
            potionImg.sprite = potionMitad;
        }

        if(pocionVida == 0)
        {
            potionImg.sprite = potionNull;
        }
    }

    private void UsarPocionVida()
    {

        if(pocionVida > 0 && PS.maxHealth >= PS.currentHealth + 10)
        {
            PS.DecreaseHealth(-10);
            pocionVida --;
        }
    }
}
