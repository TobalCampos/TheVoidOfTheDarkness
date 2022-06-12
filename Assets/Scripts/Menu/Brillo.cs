using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image panelBrillo;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brillo", 0.2f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g,panelBrillo.color.b,sliderValue);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo",sliderValue);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue);
    }
}
