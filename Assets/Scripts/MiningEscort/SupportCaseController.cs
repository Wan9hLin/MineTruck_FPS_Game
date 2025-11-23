using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SupportCaseController : MonoBehaviour
{
    public List<GameObject> bulletType = new List<GameObject>();

    public Animator chest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Support")
        {   
            chest.SetTrigger("open");
            Destroy(other.gameObject);
            StartCoroutine("RandomBulletType");
        }
    }



    IEnumerator RandomBulletType()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, 4);
            Instantiate(bulletType[randomIndex],transform.position,quaternion.identity).GetComponent<Rigidbody>().AddForce(Vector3.up*5f,ForceMode.VelocityChange);
        }
    }
    
}
