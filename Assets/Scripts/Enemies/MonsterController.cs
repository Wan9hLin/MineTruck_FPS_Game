using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public int HP;

    public float attackRange;
    
    public bool isChasing;

    public bool isWalking;

    public bool isDead;

    public bool isRemote;

    public float atteckRate;

    public float timer;

    public string aimTag;

    public GameObject bloodEffect;
    
    //受击动画
    public bool isInvincible;

    public float invincibleRate;

    public float invincibleTimer;
    
    //远程怪
    public GameObject remoteProjectile;
    public Transform shootPoint;
    //
    public int HpCapacity;

    public bool isRecallByBoss;
    //伤害
    public int damage;

    public bool isBoss;
    // Start is called before the first frame update
    void Start()
    {
        HpCapacity = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (isChasing)
            {
                WalkToAim();
            }
            
        }
        
        if (timer < atteckRate)
        {
            timer += Time.deltaTime;
        }

        if (invincibleTimer < invincibleRate)
        {
            invincibleTimer += Time.deltaTime;
            isInvincible = true;
        }
        else
        {
            isInvincible = false;
        }
    }

    public void WalkToAim()
    {
        Vector3 tagPosition = GameObject.FindWithTag(aimTag).transform.position;
    
            if (Vector3.Distance(tagPosition,gameObject.transform.position)>attackRange)
            {
                if (GetComponent<NavMeshAgent>().isActiveAndEnabled)
                {
                    try
                    {
                        GetComponent<NavMeshAgent>().destination = tagPosition;
                        GetComponent<Animator>().SetBool("isWalk",true);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Move");
                    }
                }

            }
            else
            {   
                if (GetComponent<NavMeshAgent>().isActiveAndEnabled)
                {
                    try
                    {
                        GetComponent<NavMeshAgent>().destination = gameObject.transform.position;
                        GetComponent<Animator>().SetBool("isWalk",false);
                        Attack();
                    }
                    catch (Exception e)
                    {
                        Debug.Log("attack");
                    }
                }
               
            }
        
    }

    public void Attack()
    {   
        if(timer < atteckRate)return;
        if (!isRemote)
        {
             transform.LookAt(GameObject.FindWithTag(aimTag).transform.position);
             GetComponent<Animator>().CrossFadeInFixedTime("Attack",0.02f);
             if (aimTag=="Player")
             {
                 GameObject.FindWithTag("Player").GetComponent<PlayerHpController>().TakeDanmage(damage);
             }
             
             if (aimTag=="Car")
             {
                 CarHealthController.instance.DamageEnemy(damage);
             }
        }
        else
        {   
            //远程攻击
            Vector3 tagPosition = GameObject.FindWithTag(aimTag).transform.position;
            transform.LookAt(tagPosition);
            GameObject projectile = Instantiate(remoteProjectile, shootPoint.position, Quaternion.identity);
            projectile.transform.LookAt(tagPosition);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 20f,ForceMode.Impulse);
            GetComponent<Animator>().CrossFadeInFixedTime("Attack",0.02f);
        }
       
        timer = 0;
    }

    public void TakeDamage(int damage,Vector3 position)
    {   
        HP -= damage;
        if (HP <= 0)
        {
            if (!isDead)
            {
                if (isBoss)
                {
                    UIManager.instance.SetBossHealth(0,HpCapacity);
                }
                
                Died();
            }
            
        }
        else if(HP >0)
        {
            Destroy(Instantiate(bloodEffect, position, Quaternion.identity),1f);
            if (!isInvincible)
            {
                invincibleTimer = 0;
                GetComponent<Animator>().CrossFadeInFixedTime("TakeDamage",0.5f);
                StartCoroutine(TakeDamageDelay());
            }

            if (isBoss)
            {
                UIManager.instance.SetBossHealth(HP,HpCapacity);
            }
           
        }

    }

    public void Died()
    {
        if(!isDead)GetComponent<Animator>().SetTrigger("isDead");
        isDead = true;
        GetComponent<NavMeshAgent>().destination = gameObject.transform.position;
        if (isRecallByBoss)
        {
            GameObject.Find("Boss").GetComponent<BossController>().childList.RemoveAt(0);
        }
        Destroy(gameObject,5f);
        if (isBoss)
        {
            StartCoroutine("BossDied");
        }

    }

    IEnumerator TakeDamageDelay()
    {
        isChasing = false;
        if (GetComponent<NavMeshAgent>().isActiveAndEnabled)
        {
            GetComponent<NavMeshAgent>().destination = gameObject.transform.position;    
        }
        
        yield return new WaitForSeconds(1f);
        if (GetComponent<BossController>())
        {
            if (!GetComponent<BossController>().isHealing)
            {
                isChasing = false;
                GetComponent<NavMeshAgent>().destination = transform.position;
            }
        }
        else
        {
            isChasing = true;
        }
        
    }
    
    //通关
    IEnumerator BossDied()
    {
        yield return new WaitForSeconds(3f);
        
        GameManager.instance.EndGamePanel();
    }
}
