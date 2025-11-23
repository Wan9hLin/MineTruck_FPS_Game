using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class BossController : MonoBehaviour
{
    public float totalHp;
    
    public int level=1;

    public int currentHp;

    public GameObject levelUpEffect;

    public float invincibleTime = 2f;

    public bool isHealing=false;

    public GameObject healEffect;

    public GameObject spellCallEffect;
    //三个怪打掉，这个destroy
    public GameObject healObj;

    public GameObject child;
    
    public List<int> childList = new List<int>(3);
    // Start is called before the first frame update

    private float rushRate=10f;
    
    public float timer;

    public int healTimes=0;
    void Start()
    {
        totalHp = GetComponent<MonsterController>().HP;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHp = GetComponent<MonsterController>().HP;
        if (currentHp < totalHp && currentHp > totalHp/3*2)
        {
            level = 1;

        }else if (currentHp <= totalHp/3*2 && currentHp >= totalHp/3*1)
        {
            if (level == 1)
            {
                level = 2;
                LevelUpEffect();
            }
            
        }else if (currentHp < totalHp/3*1)
        {
            if (level == 2)
            {
                level = 3;
                LevelUpEffect();
            }
        }
        
        
        switch (level)
        {
            case 1:
                Rush();
                break;
            case 2:
                //远程攻击
                RemoteMode();
                break;
            case 3:
                RemoteMode();
                HealUp();
                break;
        }

        if (timer < rushRate)
        {
            timer += Time.deltaTime;
        }
        
    }

    public void LevelUpEffect()
    {
        StartCoroutine("Invincible");
        Destroy(Instantiate(levelUpEffect,transform.position,quaternion.identity),1f);
        GetComponent<Animator>().CrossFadeInFixedTime("LevelUp",0.5f);
    }

    IEnumerator Invincible()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MonsterController>().isChasing = false;
        yield return new WaitForSeconds(invincibleTime);
        GetComponent<Collider>().enabled = true;
        if (!isHealing)
        {
            GetComponent<MonsterController>().isChasing = true;
        }
        
    }
    
    //增加血量
    public void HealUp()
    {
        if (currentHp < totalHp / 3 * 0.2f)
        {
            if (healTimes==0)
            {
                isHealing = true;
                //无敌+停止
                GetComponent<Collider>().enabled = false;
                GetComponent<MonsterController>().isChasing = false;
                GetComponent<NavMeshAgent>().destination = transform.position;
        
                //
                GetComponent<Animator>().SetBool("isWalk",false);
                GetComponent<Animator>().SetBool("isDenfend",true);
                healObj = Instantiate(healEffect, transform.position, quaternion.identity);
        
                //召唤三个小怪
        
                for (int i = 0; i < 3; i++)
                {
                    Vector3 position = new Vector3(transform.position.x+Random.Range(-2f,2f),transform.position.y+2f,transform.position.z+Random.Range(-2f,2f));
                    Destroy(Instantiate(spellCallEffect, position,Quaternion.identity),2f);
                    Instantiate(child, position, Quaternion.identity);
                    childList.Add(1);
                }
                //检查召唤的小怪有没有死
                StartCoroutine("CheckIfChildKilled");
            }

            healTimes++;
            
        }
        
    }
    
    public void RemoteMode()
    {
        if (!isHealing)
        {
             int rush = Random.Range(0, 2);
            if (rush == 0)
            {
                Rush();
            }
            GetComponent<MonsterController>().isRemote = true;
            GetComponent<MonsterController>().atteckRate = 1;
            GetComponent<MonsterController>().attackRange = 15;
        }
       
    }
    public void Rush()
    {   
        
        if (timer<rushRate)return;
        if (!isHealing)
        {
             Debug.Log("Rush"); 
             StartCoroutine("RushToPlayer");
        }
       
        timer = 0;
    }
    IEnumerator CheckIfChildKilled()
    {

        while (true)
        {
            if (childList.Count == 0)
            {
                isHealing = false;
                GetComponent<MonsterController>().isChasing = true;
                GetComponent<Collider>().enabled = true;
                Destroy(healObj);
                GetComponent<Animator>().SetBool("isDenfend",false);
                break;
            }
            
            //boss 回血
            if (currentHp/3 < totalHp /3)
            {   
                Debug.Log("加血");
                GetComponent<MonsterController>().isChasing = false;
                GetComponent<Animator>().SetBool("isWalking",false);
                GetComponent<Animator>().SetBool("isRun",false);
                GetComponent<Animator>().SetBool("isDenfend",true);
                UIManager.instance.SetBossHealth(currentHp,GetComponent<MonsterController>().HpCapacity);
                GetComponent<MonsterController>().HP += 2;
                yield return new WaitForSeconds(1);
            }
            
            yield return null;
        }
    }

    // public void RushToPlayer()
    // {
    //     if (timer<specailSkillRate)return;
    //     
    //     
    //     GetComponent<MonsterController>().isChasing = false;
    //     if (Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position) < 3f)
    //     {
    //         GetComponent<NavMeshAgent>().speed += 4;
    //         GetComponent<NavMeshAgent>().destination = transform.position;
    //         
    //     }
    //     else
    //     {
    //         GetComponent<MonsterController>().isChasing = true;
    //         GetComponent<NavMeshAgent>().speed -= 4;
    //     }
    // }
    IEnumerator ChaseDelay()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<MonsterController>().isChasing = true;
    }
    
    IEnumerator RushToPlayer()
    {
        GetComponent<MonsterController>().isChasing = false;
        //播放动画
        GetComponent<Animator>().CrossFadeInFixedTime("Roar",0.05f);
        GetComponent<Animator>().SetBool("isRun",true);
        Vector3 playerPositon = GameObject.FindWithTag("Player").transform.position;
        GetComponent<NavMeshAgent>().destination = playerPositon;
        
        while (true)
        {
            if (Vector3.Distance(playerPositon, transform.position) > 3f)
            {
                GetComponent<NavMeshAgent>().speed = 6;
            }
            else
            {
                
                GetComponent<NavMeshAgent>().speed = 3;
                
                GetComponent<Animator>().SetBool("isRun",false);
                
                GetComponent<Animator>().CrossFadeInFixedTime("Jump",0.05f);
                
                //伤害计算
                Collider[] colliderList = Physics.OverlapSphere(transform.position, 4f);
                for (int i = 0; i < colliderList.Length; i++)
                {
                    Rigidbody rb = colliderList[i].GetComponent<Rigidbody>();
                    if (rb != null)
                    {   
                        rb.AddExplosionForce(10f,transform.position,4f);
                    }

                    if (colliderList[i].transform.tag == "Player")
                    {
                        //给玩家伤害
                        Debug.Log("跳跃伤害");
                        PlayerHpController.instance.TakeDanmage(20);
                    }
                }
                StartCoroutine("ChaseDelay");
                
                break;
            }
            
            yield return null;
        }

       
    }

    
    
    
}
