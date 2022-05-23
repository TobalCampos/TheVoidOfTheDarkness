using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;

    public float currentHealth;

    private GameManager GM;
    [SerializeField]
    private GameObject deadCanvas;

    [SerializeField]
    private AudioClip deathSound;

    public Slider Slider;
    public Color Low;
    public Color High;

    public static PlayerStats instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        maxHealth = PlayerPrefs.GetFloat("HP",100f);
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetHealth(currentHealth,maxHealth);
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        SetHealth(currentHealth,maxHealth);

        if(currentHealth <= 0.0f)
        {
            soundManager.instance.PlaySound(deathSound);
            Die();
        }
    }

    private void Die()
    {   
        PlayerPrefs.SetFloat("HP",maxHealth);
        Destroy(gameObject);
        GM.Respawn();
    }
    
    public void SetHealth(float health, float maxHealth)
    {
        Slider.maxValue = maxHealth;
        Slider.value = health;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
        
    }
    
}
