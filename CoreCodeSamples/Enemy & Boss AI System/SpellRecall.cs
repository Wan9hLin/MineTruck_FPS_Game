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
 
    public int monsterNumber;
    public GameObject spawnChild;
    


    // Update is called once per frame
    void Update()
    {
        // Update the cooldown timer
        if (timer < spellRate)
        {
            timer += Time.deltaTime;
        }

        startSpell();
    }

    // Handles the spellcasting logic
    public void startSpell()
    {
        if(timer<spellRate)return;

        // Check if the boss is not dead
        if (!GetComponent<MonsterController>().isDead)
        {
            GetComponent<Animator>().CrossFadeInFixedTime("Spell",0.1f);
        
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                // Play the summon sound effect (could be added later)
                for (int i = 0; i < monsterNumber; i++)
                {
                    Vector3 position = new Vector3(hit.point.x+Random.Range(-5f,5f),hit.point.y+1f,hit.point.z+Random.Range(-5f,5f));
                    GameObject effectCircle = Instantiate(spellEffect, position,Quaternion.identity);
                    effectCircle.transform.rotation = Quaternion.LookRotation(hit.normal);
                    Destroy(effectCircle,2f);

                    Instantiate(spawnChild, position, Quaternion.identity);
                }
            
            }

            timer = 0;
        }
        

    }


}
