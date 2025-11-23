using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCotroller : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public Rigidbody theRB;
    public GameObject impactEffect;
    public int damage;

    public bool damageEnemy, damagePlayer;


    public int damageAmount = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;//will move the bullet in constant speed;
        lifeTime -= Time.deltaTime;//counting down

        if(lifeTime <= 0)
        {
            Destroy(gameObject);//destroy him after (lifetime duration) 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        
        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            PlayerHpController.instance.TakeDanmage(damage);
        }

        Destroy(gameObject);//it will be destroyed, when it hits sth
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime) * 2f), transform.rotation);
    }
}
