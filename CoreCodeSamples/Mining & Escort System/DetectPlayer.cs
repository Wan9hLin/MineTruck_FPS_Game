using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Follower follower;
 
    // Start is called before the first frame update
    void Start()
    {
        follower = GetComponent<Follower>();
        follower.enabled = false;               
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the area, enable the movement and trigger related actions
        if (other.gameObject.tag == "Player")
        {
            follower.enabled = true;
            changeWheelStatus(true);
            SoundManager.instance.PlaySFX(9);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        // If the player exits the area, disable the movement and stop wheel rotation
        if (other.gameObject.tag == "Player")
        {
            follower.enabled = false;
            changeWheelStatus(false);
        }
    }

    // Change the rotation status of all wheels (enabled or disabled)
    public void changeWheelStatus(bool status)
    {
        GameObject[] whleelList = GameObject.FindGameObjectsWithTag("wheel");
        for (var i = 0; i < whleelList.Length; i++)
        {
            whleelList[i].GetComponent<RotatedWheel>().isEnable = status;
        }
        
    }
}
