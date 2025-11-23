using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSupportChest : MonoBehaviour
{
    public Animator chest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open()
    {
        chest.SetTrigger("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Support")
        {
            
        }
    }
}
