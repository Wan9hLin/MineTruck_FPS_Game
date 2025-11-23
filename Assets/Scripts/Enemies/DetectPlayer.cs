using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    Follower follower;
 
    // Start is called before the first frame update
    void Start()
    {
        follower = GetComponent<Follower>();
        follower.enabled = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            follower.enabled = true;
            changeWheelStatus(true);
            SoundManager.instance.PlaySFX(9);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            follower.enabled = false;
            changeWheelStatus(false);
        }
    }

    public void changeWheelStatus(bool status)
    {
        GameObject[] whleelList = GameObject.FindGameObjectsWithTag("wheel");
        for (var i = 0; i < whleelList.Length; i++)
        {
            whleelList[i].GetComponent<RotatedWheel>().isEnable = status;
        }
        
    }
}
