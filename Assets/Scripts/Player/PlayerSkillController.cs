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

    public GameObject airRaidsObj;
    //effect
    public GameObject airRaidBurnEffect;

    public GameObject airRaidExplodeEffect;

    public GameObject airRaidsAreaWaring;

    public float airRaidsRange;

    public GameObject skill1;

    public GameObject skill2;

    public GameObject skill3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {   
        //grenade
        if (Input.GetKeyDown(KeyCode.E))
        {
            // StartCoroutine("Grenade");
            Grenade();
        }
        //bullet support
        if (Input.GetKeyDown(KeyCode.Q))
        {   
            Debug.Log("buji");
            SupportCasing();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {   
            Debug.Log("kong xi ");
            AirRaids();
        }
    }

    // IEnumerator Grenade()
    // {
    //     Animator grenadeAn= GameObject.FindWithTag("Gun").GetComponent<Animator>();
    //     grenadeAn.CrossFadeInFixedTime("grenade",0.1f);
    //     // StartCoroutine("createGrenade");
    //     yield return new WaitForSeconds(0.2f);
    //     GameObject grenadeObj = Instantiate(grenade, throwPoint.position, throwPoint.rotation);
    //     grenadeObj.GetComponent<Rigidbody>().AddForce(transform.forward * force+ transform.up * force,ForceMode.VelocityChange);
    //     Debug.Log("grenade");
    //
    // }
    public void Grenade()
    {   
        if(skill2.GetComponent<SkillUIManager>().isCoolDown)return;
        skill2.GetComponent<SkillUIManager>().isSuccess = true;
        Animator grenadeAn= GameObject.FindWithTag("Gun").GetComponent<Animator>();
        grenadeAn.CrossFadeInFixedTime("grenade",0.3f);
        // StartCoroutine("createGrenade");
        GameObject grenadeObj = Instantiate(grenade, throwPoint.position, throwPoint.rotation);
        grenadeObj.GetComponent<Rigidbody>().AddForce(transform.forward * force+ transform.up * force,ForceMode.VelocityChange);
        // Invoke("createGrenade",0.1f);
        Debug.Log("grenade");
    }

    // public void createGrenade()
    // {
    //     GameObject grenadeObj = Instantiate(grenade, throwPoint.position, throwPoint.rotation);
    //     grenadeObj.GetComponent<Rigidbody>().AddForce(transform.forward * force+ transform.up * force,ForceMode.VelocityChange);
    // }
    // IEnumerator createGrenade()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     GameObject grenadeObj = Instantiate(grenade, throwPoint.position, throwPoint.rotation);
    //     grenadeObj.GetComponent<Rigidbody>().AddForce(transform.forward * force+ transform.up * force,ForceMode.VelocityChange);
    // }

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

    public void AirRaids()
    {   
        if(skill3.GetComponent<SkillUIManager>().isCoolDown)return;
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, airRaidsRange))
        {
            if (hit.transform.tag == "Ground")
            {   
                //燃烧弹数量
                StartCoroutine(RocketQueue((5,hit.point)));
                // StartCoroutine(rocketFallDown(hit.point));
                // StartCoroutine(rocketFallDown(new Vector3(hit.point.x,hit.point.y,hit.point.z+Random.Range(-20,20))));
                // StartCoroutine(rocketFallDown(new Vector3(hit.point.x,hit.point.y,hit.point.z+Random.Range(-30,30))));
                skill3.GetComponent<SkillUIManager>().isSuccess = true;
            }
            else
            {
                skill3.GetComponent<SkillUIManager>().isSuccess = false;
            }
        }
    }

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

    // private void OnTriggerEnter()
    // {
    //     switch (collision.transform.tag)
    //     {
    //         case "bullet_type1":
    //             Addbullet(1, collision.transform.gameObject);
    //             break;
    //         case "bullet_type2":
    //             Addbullet(2, collision.transform.gameObject);
    //             break;
    //         case "bullet_type3":
    //             Addbullet(3, collision.transform.gameObject);
    //             break;
    //         case "bullet_type4":
    //             Healing();
    //             break;
    //             
    //         
    //     }
    // }
    //增加子弹数量
    public void Addbullet(int type,GameObject obj)
    {   
        //调用 增加ammo 函数
        //WeaponContoller.instance.gunList[WeaponContoller.instance.currentIndex].GetComponent<GunController>().totalAmmo
        switch (type)
        {
            case 1:
                if ((WeaponContoller.instance.gunList[2].GetComponent<GunController>().totalAmmo+10)<WeaponContoller.instance.gunList[2].GetComponent<GunController>().ammoCapacity)
                {
                    WeaponContoller.instance.gunList[2].GetComponent<GunController>().totalAmmo += 10;
                }
                else
                {
                    WeaponContoller.instance.gunList[2].GetComponent<GunController>().totalAmmo = WeaponContoller.instance.gunList[2].GetComponent<GunController>().ammoCapacity;
                }

                if (WeaponContoller.instance.currentIndex == 2)
                {
                    UIManager.instance.SetAmmo(WeaponContoller.instance.gunList[2].GetComponent<GunController>().currentBullet,WeaponContoller.instance.gunList[2].GetComponent<GunController>().totalAmmo);    
                }
                break;
                
            case 2:
                if ((WeaponContoller.instance.gunList[1].GetComponent<GunController>().totalAmmo+20)<WeaponContoller.instance.gunList[1].GetComponent<GunController>().ammoCapacity)
                {
                    WeaponContoller.instance.gunList[1].GetComponent<GunController>().totalAmmo += 20;
                }
                else
                {
                    WeaponContoller.instance.gunList[1].GetComponent<GunController>().totalAmmo = WeaponContoller.instance.gunList[1].GetComponent<GunController>().ammoCapacity;
                }
                
                if (WeaponContoller.instance.currentIndex == 1)
                {
                    UIManager.instance.SetAmmo(WeaponContoller.instance.gunList[1].GetComponent<GunController>().currentBullet,WeaponContoller.instance.gunList[1].GetComponent<GunController>().totalAmmo);  
                }
                
                break;
            
            case 3:
                if ((WeaponContoller.instance.gunList[0].GetComponent<GunController>().totalAmmo+ 80)<WeaponContoller.instance.gunList[0].GetComponent<GunController>().ammoCapacity)
                {
                    WeaponContoller.instance.gunList[0].GetComponent<GunController>().totalAmmo += 80;
                }
                else
                {   
                    
                    WeaponContoller.instance.gunList[0].GetComponent<GunController>().totalAmmo = WeaponContoller.instance.gunList[0].GetComponent<GunController>().ammoCapacity;
                }

                if (WeaponContoller.instance.currentIndex == 3)
                {
                    UIManager.instance.SetAmmo(WeaponContoller.instance.gunList[0].GetComponent<GunController>().currentBullet,WeaponContoller.instance.gunList[0].GetComponent<GunController>().totalAmmo);
                }
                
                break;
        }
        
        Destroy(obj);
    }

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
       
        // while (true)
        // {
        //     if (Vector3.Distance(hit, rocketObj.transform.position) < 0.1f)
        //     {
        //         Debug.Log("hit");
        //         yield return null;
        //     }
        // }
    }
    //一次发多个
    IEnumerator RocketQueue((int times,Vector3 hit)parameters)
    {
        Vector3 postion = parameters.hit;
        for (int i = 1; i <= parameters.times; i++)
        {   
            yield return new WaitForSeconds(0.5f);
            postion += Vector3.left *i;
            //生成区域警告
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
