using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceScript : MonoBehaviour
{
    public Image expImage;
    public Text currentLVLtext;
    [SerializeField] public float currentExperience, expTNL;
    [SerializeField] public int currentLVL;

    public static ExperienceScript instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {        
        currentExperience = PlayerPrefs.GetFloat("BackgroundEXP",0f);
        currentLVL = PlayerPrefs.GetInt("LvlExp",1);
        expTNL = 50.0f * currentLVL;

        currentLVLtext.text = currentLVL.ToString();
        expImage.fillAmount = currentExperience / expTNL;   
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void expModifier(float experience)
    {
        currentExperience += experience;
        expImage.fillAmount = currentExperience / expTNL; 

        PlayerPrefs.SetFloat("BackgroundEXP",currentExperience);

        if(currentExperience >= expTNL)
        {
            expTNL = expTNL + 50;
            currentExperience = 0;
            expImage.fillAmount = currentExperience / expTNL; 
            PlayerStats.instance.maxHealth += 10f;
            PlayerStats.instance.DecreaseHealth(-10);
            currentLVL++;
            currentLVLtext.text = currentLVL.ToString();
            PlayerStats.instance.GuardarVida(); 
            PlayerPrefs.SetInt("LvlExp",currentLVL);
            PlayerPrefs.SetFloat("BackgroundEXP",currentExperience);
        }
    }
}
