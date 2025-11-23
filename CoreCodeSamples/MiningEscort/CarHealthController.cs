using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHealthController : MonoBehaviour
{
    public static CarHealthController instance;
    public int currentHealth, maxHealth;
    public GameObject CarUI;
   
    public float invincibleLength = 1f;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;        

    }

    // Method to apply damage to the car
    public void DamageEnemy(int damageAmount)
    {    
        currentHealth -= damageAmount;
         CarUIController.instance.healthSlider.value = currentHealth;
         Debug.Log(currentHealth);
         if (currentHealth <= 0)
         {
             Destroy(gameObject);
             Destroy(CarUI);
             GameManager.instance.LosePanel();

         }
            
        
    }
}
