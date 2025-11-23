using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public int HP = 30;
    public Animator animator;
    
    
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if(HP <= 0)
        {
            //Death anim
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            Invoke("DestoryLag", 2);

        }
        else
        {
            //Damage anim
            animator.SetTrigger("damage");
        }
    }
  
    public void DestoryLag()
    {
        Destroy(gameObject);      
    }

}
