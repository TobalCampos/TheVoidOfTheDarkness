using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Potions : MonoBehaviour
{
    [SerializeField] public int pocionVida;
    private PlayerStats PS;
    public float curacion;

    
    public Image potionImg;
    public Text textCant;
    public Sprite potionNull;
    public Sprite potionMitad;

    public float delay = 3;
    public float delayAplicar;

    void Start()
    {
        PS = GetComponent<PlayerStats>(); 
        pocionVida = 6;
        delayAplicar = 1;
    }

    void Update()
    {  
        textCant.text = "x"+ pocionVida.ToString();

        if(pocionVida <= 3 && pocionVida > 0)
        {
            potionImg.sprite = potionMitad;
        }

        if(pocionVida == 0)
        {
            potionImg.sprite = potionNull;
        }

        delay += Time.deltaTime;
    }

    // Update is called once per frame
    public void Curarse(InputAction.CallbackContext context)
    {
        
        if(pocionVida > 0 && PS.maxHealth >= PS.currentHealth + 10 && delayAplicar < delay)
        {
            PS.DecreaseHealth(curacion);
            pocionVida --;
            delay = 0;
        }    
    }

    public void CurarseTel()
    {
        
        if(pocionVida > 0 && PS.maxHealth >= PS.currentHealth + 10 && delayAplicar < delay)
        {
            PS.DecreaseHealth(curacion);
            pocionVida --;
            delay = 0;
        }    
    }
}