using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public Image coolDownFill;
    public Image keyCoolDownFill;
    public KeyCode skillKey;
    public float coolDown;
    public bool isCoolDown;
    private float nextUseTime = 0;
    public bool isSuccess;
    void Start()
    {
        coolDownFill.fillAmount = 0.0f;
    }

    void Update()
    {
        SkillCoolDown(coolDown);
    }

    // call this function in update()
    public void SkillCoolDown(float coolDownTime)
    {
        nextUseTime -= Time.deltaTime;


        if (isCoolDown)
        {
            if (nextUseTime <= 0f)
            {
                isCoolDown = false;
                coolDownFill.fillAmount = 0f;
                keyCoolDownFill.gameObject.SetActive(false);
                isSuccess = false;
            }
            else
            {
                coolDownFill.fillAmount = nextUseTime / coolDownTime;
                keyCoolDownFill.gameObject.SetActive(true);
            }
        }

        else  // !isCoolDown
        {
            if (Input.GetKeyDown(skillKey) && isSuccess)
            {
                Debug.Log("skill used, on cooldown");
                isCoolDown = true;
                nextUseTime = coolDownTime;
            }
        }
    }
}
