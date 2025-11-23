using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GunController : MonoBehaviour
{   
    //一个弹夹多少子弹，固定值
    // [HideInInspector]
    public int bulletCapacity;
    //目前总共子弹
    // [HideInInspector]
    public int totalAmmo;
    //目前一个弹夹内的子弹
    public int currentBullet;
    // 总共多少子弹，固定值
    // [HideInInspector]
    public int ammoCapacity;
    
    public int shootRange;
    
    //中心瞄准点
    public Transform shootPoint;
    
    public GameObject shootBullet;
    
    public float speed;
    
    public Transform cameraPosition;
    
    public Camera camera;
    //control shoot rate
    public float shootRateTimer;

    public float shootRate;

    public float reloadTimer;

    public float reloadRate = 2f;

    public bool canAuto;

    public float recoilForceEachLenglth;

    public float currentRecoilLenglth;

    public float maxCameraMove;
    
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    
    // Start is called before the first frame update
    
    public static GunController instance;
    //枪子弹发射点
    public Transform firePoint;
    
    //new feature
    public ParticleSystem muzzleFlash;
    public Light muzzleFlashLight;
    public GameObject hitParticle;
    public GameObject bulletHole;
    public bool isRocket;
    public bool isReload=false;
    //0 fire 1 reload 2 dryfire
    public List<AudioSource> gunAudio = new List<AudioSource>();
    
    //aim 
    public bool isAim=false;
    public GameObject UI;
    public float aimViewZoomIn;
    public float aimViewZoomOut;
    //枪伤害
    public int damage;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        totalAmmo = ammoCapacity-currentBullet;
        currentBullet = bulletCapacity;
        UIManager.instance.SetAmmo(currentBullet,totalAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }else if (Input.GetKey(KeyCode.Mouse0) && canAuto)
        {
            Fire();
        }
        else
        {
            muzzleFlashLight.enabled = false;
            currentRecoilLenglth = 0;
        }

        if (shootRateTimer < shootRate)
        {
            shootRateTimer+=Time.deltaTime;
        }

        if (reloadTimer < reloadRate)
        {
            reloadTimer += Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            reload();
        }
        
        //aim 
        if (Input.GetKey(KeyCode.Mouse1))
        {
            isAim = true;
            Aim();
            UI.SetActive(false);
        }
        else
        {
            isAim = false;
            UI.SetActive(true);
            GetComponent<Animator>().SetBool("aim",false);
            StartCoroutine("CameraZoomOut");
        }

        if (totalAmmo < bulletCapacity*0.4)
        {
            UIManager.instance.isLowAmmo = true;
        }
        else
        {
            UIManager.instance.isLowAmmo = false;
        }
    }

    public void Fire()
    {   
        //stop
        if(shootRateTimer < shootRate)return;
        
        RaycastHit hit;
        if (currentBullet > 0 && !isReload)
        {   
            //播放枪声
            gunAudio[0].Play();
            //播放粒子
            muzzleFlash.Play();
            
            muzzleFlashLight.enabled = true;
            // Destroy(Instantiate(muzzleFlash, firePoint.position, quaternion.identity),0.2f);
            // Debug.Log("shoot");
            // muzzleFlash.Stop();
            
            //fire animation
            
            //rocket laucher
            if (isRocket)
            {
                RocketController.instance.rocketLauch();
                // if (!RocketController.instance.isloaded)
                // {
                //     gunAudio[2].Play();
                //     return;
                // }
            }
            
            if (!isAim)
            {   
                //play not aiming fire
                GetComponent<Animator>().CrossFadeInFixedTime("fire",0.1f);
            }
            else
            {
                //aim fire 
                GetComponent<Animator>().CrossFadeInFixedTime("aimFire",0.1f);
            }
            
            currentBullet--;
            //#############
            // UIController.instance.UpdateAmmo(gameObject);
            //更新子弹数量
            UIManager.instance.SetAmmo(currentBullet,totalAmmo);
            
            // CameraController.instance.Recoil();
            //################
            RecoilController.instance.Recoil(recoilX,recoilY,recoilZ);
            
            if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, shootRange))
            {   
                //伤害计算
                if (isRocket)
                {
                    AOE_attack(hit.transform);
                }
                GameObject hole = Instantiate(bulletHole, hit.point, Quaternion.identity, hit.transform);
                hole.transform.rotation = Quaternion.LookRotation(hit.normal);
                Destroy(hole,3f);
                GameObject hitEffect = Instantiate(hitParticle, hit.point, Quaternion.identity, hit.transform);
                hitEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
                Destroy(hitEffect,1f);
                //获得怪物controller并播放动画和计算hp
                if (hit.transform.tag=="Monster")
                {
                    hit.transform.gameObject.GetComponent<MonsterController>().TakeDamage(damage,hit.point);
                }

                if (hit.transform.tag == "Monster_type2")
                {
                    hit.transform.gameObject.GetComponent<MonsterController>().TakeDamage(damage,hit.point);
                }
                
                //第一关两个小怪
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
                }
                
                
                if (hit.transform.tag == "Spider")
                {
                    hit.transform.gameObject.GetComponent<Spider>().TakeDamage(damage);
                }
                // GameObject projectile = Instantiate(shootBullet, firePoint.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                // projectile.transform.LookAt(hit.point); //Sets the projectiles rotation to look at the point clicked
                // projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); 
                // Destroy(projectile,1f);
            }
            else
            {   
                // Vector3 line = camera.ScreenToWorldPoint(Input.mousePosition+new Vector3(0,0,100));
                // GameObject projectile = Instantiate(shootBullet, firePoint.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                // projectile.transform.LookAt(line); //Sets the projectiles rotation to look at the point clicked
                // projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); 
                // Destroy(projectile,1f);
            }
        }
        else
        {   
            
            Debug.Log("no bullet");
            
            gunAudio[2].Play();
            //WeaponContoller.instance.dryFireSoundPlay();
        }
        shootRateTimer = 0f;
    }

    public void reload()
    {   
        if(reloadTimer < reloadRate)return;
        if (totalAmmo > 0)
        {
            if (currentBullet < bulletCapacity)
            {
                int addAmmoNumber = bulletCapacity - currentBullet;
                if (totalAmmo > 0)
                {   
                    if (isRocket)
                    {
                        RocketController.instance.rocketReload();
                    }
                    if (totalAmmo > addAmmoNumber)
                    {
                        isReload = true;
                        Debug.Log("直接填充");
                        //#########
                        //WeaponContoller.instance.reloadSoundPlay();
                        currentBullet += addAmmoNumber;
                        totalAmmo -= addAmmoNumber;
                        //#############
                        // UIController.instance.UpdateAmmo(gameObject);
                        //换子弹
                        UIManager.instance.SetAmmo(currentBullet,totalAmmo);
                        //play animation
                        GetComponent<Animator>().SetTrigger("reload");
                        gunAudio[1].Play();
                        StartCoroutine("ReloadTime");
                    }
                    else
                    {   
                        isReload = true;
                        Debug.Log("子弹不够一个弹夹");
                        WeaponContoller.instance.reloadSoundPlay();
                        currentBullet += totalAmmo;
                        totalAmmo = 0;
                        //############
                        //UIController.instance.UpdateAmmo(gameObject);
                        //play animation
                        GetComponent<Animator>().SetTrigger("reload");
                        gunAudio[1].Play();
                        StartCoroutine("ReloadTime");
                    }
                }
                else
                {
                    // WeaponContoller.instance.dryFireSoundPlay();
                    GetComponent<Animator>().SetTrigger("dryFire");
                    gunAudio[2].Play();
                }
                
                
            }
            else
            {
                // WeaponContoller.instance.dryFireSoundPlay();
                GetComponent<Animator>().SetTrigger("dryFire");
                gunAudio[2].Play();
            }
        }
        else
        {   
            GetComponent<Animator>().
            GetComponent<Animator>().SetTrigger("dryFire");
            gunAudio[2].Play();
            // WeaponContoller.instance.dryFireSoundPlay();
            
           
        }

        reloadTimer = 0;

    }

    public void Aim()
    {
        if (!PlayerController.instance.isWalking)
        {
            GetComponent<Animator>().SetBool("aim",true);
            UI.SetActive(false);
            StartCoroutine("CameraZoomIn");
        }
        else
        {
            GetComponent<Animator>().SetBool("aim",false);
            UI.SetActive(true);
            StartCoroutine("CameraZoomOut");
        }
        
    }

    public void AOE_attack(Transform trans)
    {
        float radius = 10f;
        float force = 20f;
        //get all collider in area
        Collider[] colliderList = Physics.OverlapSphere(trans.position, radius);
        for (int i = 0; i < colliderList.Length; i++)
        {
            Rigidbody rb = colliderList[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                // rb.AddExplosionForce(force,transform.position,radius);
                colliderList[i].GetComponent<MonsterController>().TakeDamage(damage,colliderList[i].transform.position);
                rb.AddForce(-rb.transform.forward * force, ForceMode.Impulse);
            }
            //第一关两只小怪
            if (colliderList[i].transform.tag == "Enemy")
            {
                colliderList[i].transform.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
            }
                
                
            if (colliderList[i].transform.tag == "Spider")
            {
                colliderList[i].transform.gameObject.GetComponent<Spider>().TakeDamage(damage);
            }
            
        }
        
    }
    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(2.5f);
        isReload = false;
    }

    IEnumerator CameraZoomIn()
    {   
        
        while (camera.fieldOfView >=aimViewZoomIn)
        {
            camera.fieldOfView -= Time.deltaTime/10;
        }
        yield return 0;
    }
    
    IEnumerator CameraZoomOut()
    {   
        while (camera.fieldOfView <aimViewZoomOut)
        {
            camera.fieldOfView += Time.deltaTime/10;
        }
        yield return 0;
    }

    // public int GetbulletCapacity()
    // {
    //     return bulletCapacity;
    // }
    //
    // public int GetAmmoCapacity()
    // {
    //     return bulletCapacity;
    // }
}
