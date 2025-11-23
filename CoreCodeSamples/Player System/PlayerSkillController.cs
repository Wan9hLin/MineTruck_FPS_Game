using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSkillController : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject grenade;
    public float force;
    public GameObject supportCasingEffect;
    public Transform shootPoint;
    public float supportCasingRange;
    public GameObject supportBox;


    // Air raid effects
    public GameObject airRaidsObj;
    public GameObject airRaidBurnEffect;
    public GameObject airRaidExplodeEffect;
    public GameObject airRaidsAreaWaring;
    public float airRaidsRange;
    
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;

    void Update()
    {
        // Check for skill input (grenade, bullet support, air raids)
        if (Input.GetKeyDown(KeyCode.E)) Grenade();
        if (Input.GetKeyDown(KeyCode.Q)) SupportCasing();
        if (Input.GetKeyDown(KeyCode.X)) AirRaids();
    }

    // Throw grenade
    public void Grenade()
    {   
        if(skill2.GetComponent<SkillUIManager>().isCoolDown)return;
        skill2.GetComponent<SkillUIManager>().isSuccess = true;
        Animator grenadeAn= GameObject.FindWithTag("Gun").GetComponent<Animator>();
        grenadeAn.CrossFadeInFixedTime("grenade",0.3f);
   
        GameObject grenadeObj = Instantiate(grenade, throwPoint.position, throwPoint.rotation);
        grenadeObj.GetComponent<Rigidbody>().AddForce(transform.forward * force+ transform.up * force,ForceMode.VelocityChange);
    
        Debug.Log("grenade");
    }

    // Call in bullet support
    public void SupportCasing()
    {   
        if(skill1.GetComponent<SkillUIManager>().isCoolDown)return;
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, supportCasingRange))
        {
            if (hit.transform.tag == "Ground")
            {
                GameObject effect = Instantiate(supportCasingEffect, hit.point, quaternion.identity);
                Vector3 airPositon = new Vector3(hit.point.x,hit.point.y+30f,hit.point.z);
                GameObject box = Instantiate(supportBox,airPositon,quaternion.identity);
                skill1.GetComponent<SkillUIManager>().isSuccess = true;
            }
            else
            {
                skill1.GetComponent<SkillUIManager>().isSuccess = false;
            }
        }
        
    }

    // Call in air raids
    public void AirRaids()
    {   
        if(skill3.GetComponent<SkillUIManager>().isCoolDown)return;
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, airRaidsRange))
        {
            if (hit.transform.tag == "Ground")
            {
                // Trigger multiple air raid rockets
                StartCoroutine(RocketQueue((5,hit.point)));

                skill3.GetComponent<SkillUIManager>().isSuccess = true;
            }
            else
            {
                skill3.GetComponent<SkillUIManager>().isSuccess = false;
            }
        }
    }

    // Handle interactions with ammo pickups and health packs
    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "bullet_type1":
                Addbullet(1, other.transform.parent.gameObject);
                break;
            case "bullet_type2":
                Addbullet(2, other.transform.parent.gameObject);
                break;
            case "bullet_type3":
                Addbullet(3, other.transform.parent.gameObject);
                break;
            case "bullet_type4":
                Destroy(other.transform.parent.gameObject);
                Healing();
                break;               
            
        }
    }


    // Add ammo based on pickup type
    public void Addbullet(int type,GameObject obj)
    {   

        switch (type)
        {
            case 1: // Bullet type 1
                if (WeaponContoller.instance.gunList[2].GetComponent<GunController>().totalAmmo + 10 < WeaponContoller.instance.gunList[2].GetComponent<GunController>().ammoCapacity)
                    WeaponContoller.instance.gunList[2].GetComponent<GunController>().totalAmmo += 10;
                else
                    WeaponContoller.instance.gunList[2].GetComponent<GunController>().totalAmmo = WeaponContoller.instance.gunList[2].GetComponent<GunController>().ammoCapacity;

                if (WeaponContoller.instance.currentIndex == 2)
                    UIManager.instance.SetAmmo(WeaponContoller.instance.gunList[2].GetComponent<GunController>().currentBullet, WeaponContoller.instance.gunList[2].GetComponent<GunController>().totalAmmo);
                break;

            case 2: // Bullet type 2
                if (WeaponContoller.instance.gunList[1].GetComponent<GunController>().totalAmmo + 20 < WeaponContoller.instance.gunList[1].GetComponent<GunController>().ammoCapacity)
                    WeaponContoller.instance.gunList[1].GetComponent<GunController>().totalAmmo += 20;
                else
                    WeaponContoller.instance.gunList[1].GetComponent<GunController>().totalAmmo = WeaponContoller.instance.gunList[1].GetComponent<GunController>().ammoCapacity;

                if (WeaponContoller.instance.currentIndex == 1)
                    UIManager.instance.SetAmmo(WeaponContoller.instance.gunList[1].GetComponent<GunController>().currentBullet, WeaponContoller.instance.gunList[1].GetComponent<GunController>().totalAmmo);
                break;

            case 3: // Bullet type 3
                if (WeaponContoller.instance.gunList[0].GetComponent<GunController>().totalAmmo + 80 < WeaponContoller.instance.gunList[0].GetComponent<GunController>().ammoCapacity)
                    WeaponContoller.instance.gunList[0].GetComponent<GunController>().totalAmmo += 80;
                else
                    WeaponContoller.instance.gunList[0].GetComponent<GunController>().totalAmmo = WeaponContoller.instance.gunList[0].GetComponent<GunController>().ammoCapacity;

                if (WeaponContoller.instance.currentIndex == 3)
                    UIManager.instance.SetAmmo(WeaponContoller.instance.gunList[0].GetComponent<GunController>().currentBullet, WeaponContoller.instance.gunList[0].GetComponent<GunController>().totalAmmo);
                break;
        }
        
        Destroy(obj);
    }

    // Handle rocket falling and hitting the target
    IEnumerator rocketFallDown(Vector3 hit)
    {   
        Vector3 airPositon = new Vector3(hit.x+Random.Range(-10f,10f),hit.y+30f,hit.z+Random.Range(-15f,15f));
        GameObject rocketObj = Instantiate(airRaidsObj, airPositon, Quaternion.Euler(new Vector3(-180,0,0)));
        while (true)
        {
            if (rocketObj)
            {
                rocketObj.transform.position = Vector3.MoveTowards(rocketObj.transform.position, hit, 30f * Time.deltaTime);    
                if (Vector3.Distance(rocketObj.transform.position, hit) < 0.1f)
                {

                    // Deal damage to enemies in the explosion radius
                    Collider[] colliderList = Physics.OverlapSphere(hit, 2);
                    for (int i = 0; i < colliderList.Length; i++)
                    {
                        if (colliderList[i].transform.tag == "Enemy")
                        {
                            colliderList[i].transform.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(30);
                        }
                
                
                        if (colliderList[i].transform.tag == "Spider")
                        {
                            colliderList[i].transform.gameObject.GetComponent<Spider>().TakeDamage(30);
                        }
                    }
                    Destroy(Instantiate(airRaidBurnEffect, hit, quaternion.identity),6f);
                    Destroy(Instantiate(airRaidExplodeEffect, hit, quaternion.identity),1.5f);
                    Destroy(rocketObj);
                    break;
                    
                }
               
            }
            
            
            yield return null;
        }
  
    }

    // Trigger multiple rockets for air raids
    IEnumerator RocketQueue((int times,Vector3 hit)parameters)
    {
        Vector3 postion = parameters.hit;
        for (int i = 1; i <= parameters.times; i++)
        {   
            yield return new WaitForSeconds(0.5f);
            postion += Vector3.left *i;
            Destroy(Instantiate(airRaidsAreaWaring,parameters.hit,quaternion.identity),2f);
            StartCoroutine(rocketFallDown(postion));
        }
    }
    
    public void Healing()
    {   
        Debug.Log("healing");
        PlayerHpController.instance.Healing(20);
    }
}
