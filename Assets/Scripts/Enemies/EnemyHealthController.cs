using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageEnemy(int damageAmount)
    {
        currentHealth -= damageAmount;
        anim.SetTrigger("damage");

      
        if (currentHealth <= 0)
        {
            anim.SetTrigger("die");
            anim.SetBool("isMoving", false);  
            GetComponent<Collider>().enabled = false;
            Invoke("DestoryObj", 2);


        }
    }
    public void DestoryObj()
    {
        Destroy(gameObject);
    }

}
