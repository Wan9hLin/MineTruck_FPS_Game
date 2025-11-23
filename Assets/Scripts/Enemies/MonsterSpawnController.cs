using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawnController : MonoBehaviour
{
    public List<GameObject> monsterList = new List<GameObject>();

    public int maxMonsterNumber;
    

    public float spawnRate;

    public float spawnTimer;

    public static MonsterSpawnController instance;

    public GameObject monster;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnRate)
        {
            spawnTimer += Time.deltaTime;
        }
        spawn();
        for (int i = 0; i <monsterList.Count ; i++)
        {
            if (!monsterList[i])
            {   
                Debug.Log("remove");
                monsterList.RemoveAt(i);
            }
        }
    }
    
    public void spawn()
    {
        if(spawnTimer < spawnRate)return;
        if (monsterList.Count < maxMonsterNumber)
        {
            Vector3 position = new Vector3(transform.position.x+Random.Range(-3f,3f), transform.position.y, transform.position.z+Random.Range(-3f,3f));
            monsterList.Add(Instantiate(monster, position, Quaternion.identity));
            // currentMonsterNumber++;
        }
    }
    
    
    
    
}
