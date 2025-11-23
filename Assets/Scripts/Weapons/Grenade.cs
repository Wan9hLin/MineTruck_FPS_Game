using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionEffect;

    public float explosionDelayTime;

    public int harmValue;

    public AudioSource sound;

    public float radius;

    public float explosionForce;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GrenadeDelay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GrenadeDelay()
    {
        yield return new WaitForSeconds(explosionDelayTime);
        grenadeExplode();
    }

    public void grenadeExplode()
    {
        harmCalculate();
        Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity),2f);
        gameObject.SetActive(false);
        Destroy(gameObject,1f);
    }

    public void harmCalculate()
    {
        //get all collider in area
        Collider[] colliderList = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < colliderList.Length; i++)
        {
            Rigidbody rb = colliderList[i].GetComponent<Rigidbody>();
            if (rb != null)
            {   
                rb.AddExplosionForce(explosionForce,transform.position,radius);
            }

            if (colliderList[i].transform.tag == "Monster")
            {
                colliderList[i].GetComponent<MonsterController>().TakeDamage(harmValue,colliderList[i].transform.position);
            }
            
            if (colliderList[i].transform.tag == "Enemy")
            {
                colliderList[i].transform.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(harmValue);
            }
                
                
            if (colliderList[i].transform.tag == "Spider")
            {
                colliderList[i].transform.gameObject.GetComponent<Spider>().TakeDamage(harmValue);
            }
        }
    }

}
