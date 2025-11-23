using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRepairEnable : MonoBehaviour
{

    public CarRepairModule carRepairModule;
    public GameObject fillarea;


    void Start()
    {
        carRepairModule.isEnabled = false;
        fillarea.SetActive(false);        
    }


    void Update()
    {
        // Enable repair process when the player presses the 'G' key
        if (Input.GetKeyDown(KeyCode.G))
        {

            EnableRepair();         

        }

        // Disable the repair process after 4 successful repairs
        if (carRepairModule.successTimes == 4)
        {
            Invoke("DisableRepair", 1);
            
        }


    }

    public void EnableRepair()
    {
        fillarea.SetActive(true);
        carRepairModule.isEnabled = true;

    }


    public void DisableRepair()
    {
        fillarea.SetActive(false);
        carRepairModule.isEnabled = false;
        carRepairModule.successTimes = 0;
        carRepairModule.isAdd = true;
    }

}
