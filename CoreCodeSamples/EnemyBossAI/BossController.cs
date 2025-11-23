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

    public GameObject healObj;
    public GameObject child;   
    public List<int> childList = new List<int>(3);

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

        // Level up based on health thresholds
        if (currentHp < totalHp && currentHp > totalHp/3*2)
        {
            level = 1;

        }
        
        else if (currentHp <= totalHp/3*2 && currentHp >= totalHp/3*1)
        {
            if (level == 1)
            {
                level = 2;
                LevelUpEffect();
            }
            
        }
        
        else if (currentHp < totalHp/3*1)
        {
            if (level == 2)
            {
                level = 3;
                LevelUpEffect();
            }
        }

        // Switch between boss abilities based on current level
        switch (level)
        {
            case 1:
                Rush();
                break;
            case 2:
                RemoteMode();
                break;
            case 3:
                RemoteMode();
                HealUp();
                break;
        }

        // Update the rush attack timer
        if (timer < rushRate)
        {
            timer += Time.deltaTime;
        }
        
    }

    // Trigger level-up effect and invincibility phase
    public void LevelUpEffect()
    {
        StartCoroutine("Invincible");
        Destroy(Instantiate(levelUpEffect,transform.position,quaternion.identity),1f);
        GetComponent<Animator>().CrossFadeInFixedTime("LevelUp",0.5f);
    }

    // Invincibility logic for the boss during level-up
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

    // Heal the boss and summon minions when health is low
    public void HealUp()
    {
        if (currentHp < totalHp / 3 * 0.2f)
        {
            if (healTimes==0)
            {
                isHealing = true;

                GetComponent<Collider>().enabled = false;
                GetComponent<MonsterController>().isChasing = false;
                GetComponent<NavMeshAgent>().destination = transform.position;
        
                GetComponent<Animator>().SetBool("isWalk",false);
                GetComponent<Animator>().SetBool("isDenfend",true);
                healObj = Instantiate(healEffect, transform.position, quaternion.identity);

                // Summon three minions around the boss
                for (int i = 0; i < 3; i++)
                {
                    Vector3 position = new Vector3(transform.position.x+Random.Range(-2f,2f),transform.position.y+2f,transform.position.z+Random.Range(-2f,2f));
                    Destroy(Instantiate(spellCallEffect, position,Quaternion.identity),2f);
                    Instantiate(child, position, Quaternion.identity);
                    childList.Add(1);
                }

                StartCoroutine("CheckIfChildKilled");
            }

            healTimes++;
            
        }
        
    }

    // Enable ranged attacks for the boss and randomize attack modes
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

    // Perform a rush attack towards the player
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

    // Check if minions are killed and stop healing if necessary
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

            // Heal the boss if health is below a certain threshold
            if (currentHp/3 < totalHp /3)
            {   
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

    // Delay the chase to allow the rush animation to complete
    IEnumerator ChaseDelay()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<MonsterController>().isChasing = true;
    }

    // Execute the rush attack towards the player
    IEnumerator RushToPlayer()
    {
        GetComponent<MonsterController>().isChasing = false;

        GetComponent<Animator>().CrossFadeInFixedTime("Roar",0.05f);
        GetComponent<Animator>().SetBool("isRun",true);
        Vector3 playerPositon = GameObject.FindWithTag("Player").transform.position;
        GetComponent<NavMeshAgent>().destination = playerPositon;

        // Move towards the player
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

                // Deal damage in the area of effect
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
                        // Deal damage to the player       
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
