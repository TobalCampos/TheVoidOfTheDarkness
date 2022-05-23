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
        PlayerPrefs.SetInt("LvlExp",currentLVL);

        if(currentExperience >= expTNL)
        {
            expTNL = expTNL + 50;
            currentExperience = 0;
            PlayerStats.instance.maxHealth += 10f;
            PlayerStats.instance.DecreaseHealth(-10);
            currentLVL++;
            currentLVLtext.text = currentLVL.ToString();

        }
    }
}
