using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detecting : MonoBehaviour
{
    public progressBar progress;
    public GameObject MineSlider;
    public GameObject Mineobj;
   
    // Start is called before the first frame update
    void Start()
    {
        MineSlider.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Mine")
        {
            progress.GetCurrentMineObj(other.gameObject);
            MineSlider.SetActive(true);
            progress.isEnable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Mine")
        {
            
            MineSlider.SetActive(false);
            progress.isEnable = false;
            progress.slider.value = 0;
            
        }
    }
}
