using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRepairEnable : MonoBehaviour
{
    //public CarHealthController carHealthController;
    public CarRepairModule carRepairModule;
    public GameObject fillarea;
    // Start is called before the first frame update
    void Start()
    {
        carRepairModule.isEnabled = false;
        fillarea.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {

            EnableRepair();

           

        }
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
