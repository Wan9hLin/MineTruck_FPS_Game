using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirRaidsFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" )
        {
            //伤害计算
            PlayerHpController.instance.TakeDanmage(1);
        }
        
        if (other.transform.tag == "Enemy")
        {   
            
            other.transform.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(5);
        }
                
                
        if (other.transform.tag == "Spider")
        {
            other.transform.gameObject.GetComponent<Spider>().TakeDamage(5);
            Debug.Log("spider");
        }

        if (other.transform.tag == "Monster")
        {
            other.transform.gameObject.GetComponent<MonsterController>().TakeDamage(5,transform.position);
        }
    }
}
