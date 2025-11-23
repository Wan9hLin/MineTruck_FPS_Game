using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CarRepairModule : MonoBehaviour
{
    // Start is called before the first frame update
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
        //carHealthController = GetComponent<CarHealthController>();
        //+1*offsetMin*60
        //0-9
        RandomArea();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
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

                
                if (isAdd)
                {
                    Addblood();
                }
                

            }
        }
        
    }

    private void PointerMoveInLoop()
    {
        if(isIncrease && carRepairSlider.value< carRepairSlider.maxValue && canMove)
        {
            carRepairSlider.value += Time.deltaTime * 15f;
        }else if(canMove)
        {
            isIncrease = false;
        }
        
        if(!isIncrease && carRepairSlider.value > carRepairSlider.minValue && canMove)
        {
            carRepairSlider.value -= Time.deltaTime * 15f;
        }
        else if(canMove)
        {
            isIncrease = true;
        }
    }

    private void RandomArea()
    {
        float offsetMin = -270f;
        float offsetMax = 270f;
        //index 0-9
        randomAreaIndex = Random.Range(0, 10);
        area.anchoredPosition = new Vector2(offsetMin + randomAreaIndex * 60, 0);
    }

    private void CheckIfPointerInArea()
    {
        if( randomAreaIndex< carRepairSlider.value && randomAreaIndex + 1 > carRepairSlider.value)
        {
            Debug.Log("success");
            successTimes++;
        }else
        {
            Debug.Log("fail");
        }
        StartCoroutine("WaitToRestart");

    }
    IEnumerator WaitToRestart()
    {
        
        yield return new WaitForSeconds(1);
        canMove = true;
        RandomArea();
    }

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
