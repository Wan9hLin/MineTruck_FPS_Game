using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public GameObject rocketProjectile;

    public static RocketController instance;

    public bool isloaded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rocketLauch()
    {
        isloaded = false;
        rocketProjectile.SetActive(false);
    }
    
    public void rocketReload()
    {
        isloaded = true;
        rocketProjectile.SetActive(true);
    }
    
    
}
