using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
public class SpellRecall : MonoBehaviour
{
    public GameObject spellEffect;

    public float timer;

    public float spellRate;
    //召唤怪的数量
    public int monsterNumber;

    public GameObject spawnChild;
    
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spellRate)
        {
            timer += Time.deltaTime;
        }
        startSpell();
    }

    public void startSpell()
    {
        if(timer<spellRate)return;
        //判断是否死亡
        if (!GetComponent<MonsterController>().isDead)
        {
            GetComponent<Animator>().CrossFadeInFixedTime("Spell",0.1f);
        
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                // NavMeshPath path = new NavMeshPath();
                // Vector3 target = GameObject.FindWithTag(GetComponent<MonsterController>().aimTag).transform.position;
                // if (NavMesh.CalculatePath(transform.position,target , NavMesh.AllAreas, path))
                // {
                //     Debug.Log(path.corners.Length);
                //     for (int i = 0; i < path.corners.Length - 1; i++)
                //     {
                //         Vector3 position = (path.corners[i] + path.corners[i + 1]) / 2;
                //         GameObject obj = Instantiate(spellEffect, position, Quaternion.identity);
                //     }
                // }
                //播放召唤音乐
                for (int i = 0; i < monsterNumber; i++)
                {
                    Vector3 position = new Vector3(hit.point.x+Random.Range(-5f,5f),hit.point.y+1f,hit.point.z+Random.Range(-5f,5f));
                    GameObject effectCircle = Instantiate(spellEffect, position,Quaternion.identity);
                    effectCircle.transform.rotation = Quaternion.LookRotation(hit.normal);
                    Destroy(effectCircle,2f);
                    //生成小怪
                    Instantiate(spawnChild, position, Quaternion.identity);
                }
            
            }

            timer = 0;
        }
        

    }


}
