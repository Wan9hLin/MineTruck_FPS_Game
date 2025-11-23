using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpController : MonoBehaviour
{
    public int HP;
    public int capacityHP;
    public static PlayerHpController instance;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        capacityHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            GameManager.instance.LosePanel();
        }
    }

    public void TakeDanmage(int damage)
    {
        
        HP -= damage;
        //调用UI扣血
        Debug.Log("Player Hp"+HP);
        UIManager.instance.SetHealth(HP);
        UIManager.instance.isHurt = true;
    }
    //加血
    public void Healing(int healAmount)
    {
        if ((HP + healAmount) > capacityHP)
        {
            HP = capacityHP;
        }
        else
        {
            HP += healAmount;
        }
        UIManager.instance.SetHealth(HP);
    }
}
