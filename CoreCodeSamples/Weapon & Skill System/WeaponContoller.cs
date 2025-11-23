using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WeaponContoller : MonoBehaviour
{
    public List<GameObject> gunList = new List<GameObject>(5);
    public int currentIndex=0;
    public static WeaponContoller instance;
    
 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gunList[0].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        changeWeapen();
    }

    // Handle weapon switching based on mouse scroll wheel input
    public void changeWeapen()
    {
        if (Input.GetAxis("Mouse ScrollWheel")<0)// Scroll down
        {              
            currentIndex++;
            if (currentIndex > gunList.Count-1)
            {
                // If we exceed the gun list, loop back to the first gun
                gunList[currentIndex-1].SetActive(false);
                currentIndex = 0;
                gunList[currentIndex].SetActive(true);
            }
            else
            {
                gunList[currentIndex-1].SetActive(false);
                gunList[currentIndex].SetActive(true);    
            }

            // Update ammo display
            UIManager.instance.SetAmmo(gunList[currentIndex].GetComponent<GunController>().currentBullet,gunList[currentIndex].GetComponent<GunController>().totalAmmo);


        }else if (Input.GetAxis("Mouse ScrollWheel") > 0)// Scroll up
        {
            currentIndex--;

            if (currentIndex < 0)
            {
                gunList[currentIndex+1].SetActive(false);
                currentIndex = gunList.Count-1;
                gunList[currentIndex].SetActive(true);
            }
            else
            {
                gunList[currentIndex+1].SetActive(false);
                gunList[currentIndex].SetActive(true);
            }

            UIManager.instance.SetAmmo(gunList[currentIndex].GetComponent<GunController>().currentBullet,gunList[currentIndex].GetComponent<GunController>().totalAmmo);
        }
    }
    
 
}
