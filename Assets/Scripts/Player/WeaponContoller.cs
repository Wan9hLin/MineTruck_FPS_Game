using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponContoller : MonoBehaviour
{
    public List<GameObject> gunList = new List<GameObject>(5);
    
    // private AudioSource changeGun,dryFire,reload;
    
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
        // changeGun = GetComponents<AudioSource>()[0];
        // dryFire =  GetComponents<AudioSource>()[1];
        // reload =  GetComponents<AudioSource>()[2];
        // UIController.instance.UpdateAmmo(gunList[currentIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        changeWeapen();
    }

    public void changeWeapen()
    {
        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {   
            
            // changeGun.Play();
            currentIndex++;
            if (currentIndex > gunList.Count-1)
            {   
                gunList[currentIndex-1].SetActive(false);
                currentIndex = 0;
                gunList[currentIndex].SetActive(true);
            }
            else
            {
                gunList[currentIndex-1].SetActive(false);
                gunList[currentIndex].SetActive(true);    
            }
            //play gun exchange animation
            
            // gunList[currentIndex].GetComponent<Animator>().SetTrigger("reload");
            // UIController.instance.UpdateAmmo(gunList[currentIndex]);
            // TextMeshProUGUI textObject;
            // string textToDisplay = gunList[currentIndex].GetComponent<GunController>().ammoCapacity.ToString();
            // UIManager.instance.maxAmmoText.text = gunList[currentIndex].GetComponent<GunController>().ammoCapacity.ToString();
            // UIManager.instance..text = gunList[currentIndex].GetComponent<GunController>().ammoCapacity.ToString();
            
            UIManager.instance.SetAmmo(gunList[currentIndex].GetComponent<GunController>().currentBullet,gunList[currentIndex].GetComponent<GunController>().totalAmmo);


        }else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentIndex--;
            // changeGun.Play();
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
            // gunList[currentIndex].GetComponent<Animator>().SetTrigger("reload");
            // UIController.instance.UpdateAmmo(gunList[currentIndex]);
            UIManager.instance.SetAmmo(gunList[currentIndex].GetComponent<GunController>().currentBullet,gunList[currentIndex].GetComponent<GunController>().totalAmmo);
        }
    }
    
    
    public void dryFireSoundPlay()
    {
        // dryFire.Play();
    }
    
    public void reloadSoundPlay()
    {
        // reload.Play();
    }
}
