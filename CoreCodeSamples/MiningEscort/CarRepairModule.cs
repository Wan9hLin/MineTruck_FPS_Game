using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CarRepairModule : MonoBehaviour
{
    public int tryTimes;
    public int successTimes;
    public RectTransform sliderLenglth;
    public Slider carRepairSlider;

    public bool canMove;
    public bool isIncrease=true;
    public bool isChange;

    public RectTransform area;
    public int randomAreaIndex;
    public CarHealthController carHealthController;

    public bool isEnabled = true;
    public bool isAdd = true;
    
    void Start()
    {  
        RandomArea();   
    }


    void Update()
    {
        if (isEnabled) // Check if the repair process is enabled
        {
            if (successTimes <= tryTimes)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    canMove = false;
                    Debug.Log(canMove);
                    CheckIfPointerInArea();
                }
                PointerMoveInLoop();
            }
            else
            {
               
                if (isAdd) // If repairs are done, add health
                {
                    Addblood();
                }
                
            }
        }
        
    }

    // Move the repair slider back and forth in a loop
    private void PointerMoveInLoop()
    {
        if(isIncrease && carRepairSlider.value< carRepairSlider.maxValue && canMove)
        {
            carRepairSlider.value += Time.deltaTime * 15f;
        }
        else if(canMove)
        {
            isIncrease = false; // Switch to decreasing the slider value
        }
        
        if(!isIncrease && carRepairSlider.value > carRepairSlider.minValue && canMove)
        {
            carRepairSlider.value -= Time.deltaTime * 15f; 
        }
        else if(canMove)
        {
            isIncrease = true; // Switch back to increasing the slider value
        }
    }

    // Randomly set the repair area index and position it
    private void RandomArea()
    {
        float offsetMin = -270f;
        float offsetMax = 270f;

        randomAreaIndex = Random.Range(0, 10);
        area.anchoredPosition = new Vector2(offsetMin + randomAreaIndex * 60, 0);
    }

    // Check if the slider is in the correct area for a successful repair
    private void CheckIfPointerInArea()
    {
        if( randomAreaIndex< carRepairSlider.value && randomAreaIndex + 1 > carRepairSlider.value)
        {
            Debug.Log("success");
            successTimes++;
        }
        else
        {
            Debug.Log("fail");
        }

        StartCoroutine("WaitToRestart");

    }

    // Coroutine to wait before allowing the player to try again
    IEnumerator WaitToRestart()
    {
        
        yield return new WaitForSeconds(1);
        canMove = true;
        RandomArea();
    }

    // Add health to the car after a successful repair
    public void Addblood()
    {
        if (carHealthController.currentHealth < 100)
        {
            carHealthController.currentHealth += 30;
            isAdd = false;
            CarUIController.instance.healthSlider.value = carHealthController.currentHealth;
        }
        else
        {
            carHealthController.currentHealth = carHealthController.maxHealth;
            isAdd = false;
            CarUIController.instance.healthSlider.value = carHealthController.currentHealth;
        }
    }

}
