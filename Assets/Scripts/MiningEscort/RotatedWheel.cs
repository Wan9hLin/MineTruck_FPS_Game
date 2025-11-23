using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatedWheel : MonoBehaviour
{
    public static RotatedWheel instance;

    Vector3 turnwheels;
    public bool isEnable = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnable)
        {
            turnwheels = new Vector3(60, 0, 0);//Rotating coin in X axis
            transform.Rotate(turnwheels * Time.deltaTime);
        }
    }
}
