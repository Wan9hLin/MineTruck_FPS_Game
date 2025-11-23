using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    
    public int maxHealth, currentHealth;

    public float invincibleLength = 1f;
    private float invicCounter;


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
        UIController.instance.healthText.text =  "Health: " + maxHealth  + "/" + currentHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(invicCounter > 0)
        {
            invicCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {

        if (invicCounter <= 0 && !GameManager.instance.ending)
        {
            currentHealth -= damageAmount;
            SoundManager.instance.PlaySFX(12);


            
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);

                currentHealth = 0;
                

                SoundManager.instance.stopBgm();
            }
            invicCounter = invincibleLength;

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        }
    }
    public void HealPlayer(int HealAmount)
    {
        currentHealth += HealAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth;

    }

}
