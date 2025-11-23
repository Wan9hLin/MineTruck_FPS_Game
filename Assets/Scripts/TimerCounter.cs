using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCounter : MonoBehaviour
{
    // public Image Liner_timer_image;
    // float timeRemain;
    // public Slider slider;
    // public float max_time = 5.0f;
    public bool isEnable = false;

    private bool isSuccess=false; 
    // Start is called before the first frame update
    void Start()
    {
        // timeRemain = max_time;
        GameObject.Find("Boss").GetComponent<MonsterController>().isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (isEnable)
        // {
        //     if (timeRemain > 0)
        //     {
        //         timeRemain -= Time.deltaTime;
        //        // Liner_timer_image.fillAmount = timeRemain / max_time;
        //         slider.value = timeRemain;
        //
        //     }
        //     else
        //     {
        //         if (!isSuccess)
        //         {
        //             isSuccess = true;
        //             
        //         }
        //     }
        // }
        if (isEnable)
        {
            UIManager.instance.RepairTimer();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isEnable = true;
            UIManager.instance.SetRepairProgressBar();
            SoundManager.instance.PlaySFX(17);
        }
    }
}
