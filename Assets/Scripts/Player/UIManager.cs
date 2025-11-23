using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //public TextMeshProUGUI healthText;
    public Slider healthSlider;
    public Image healthFill;
    public Gradient healthGradient;

    //public TextMeshProUGUI oxygenText;
    public Slider oxygenSlider;

    public Color lerpColor = Color.white;
    public bool isLowHealth = false;
    public Image healthIcon;
    public bool isLowOxygen = false;
    public Image oxygenIcon;
    public bool isLowAmmo = false;
    public TextMeshProUGUI ammoText, maxAmmoText;

    public bool isHurt = false;
    public CanvasGroup hurtPanel;

    public Slider volumeSlider;

    public bool isBoss;
    public GameObject bossWarningPanel;
    public GameObject[] bossHealthBar;

    public GameObject repairProgressBar;
    public float repairTime;
    private bool stopTimer = false;

    public bool isCollected = false;
    public GameObject collectPopup;

    public bool isClose=false;
    private void Awake()
    {
        instance = this;
        //SetRepairProgressBar();
    }

    private void Start()
    {
        SetMaxHealth(100);
    }

    void Update()
    {
        if (hurtPanel.alpha > 0)
        {
            hurtPanel.alpha -= Time.deltaTime;
        }

        LowValue();

        if (isBoss)
        {
            StartCoroutine(BossLevel());
            isBoss = false;
        }

        // if (!stopTimer)
        // {
        //     RepairTimer();
        // }

        CollectPopup();
    }

    // call in start()
    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        healthFill.color = healthGradient.Evaluate(1f);
    }

    // call in update()
    public void SetHealth(int health)
    {
        healthSlider.value = health;

        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
        //调用受击动画
        if (isHurt)
        {
            hurtPanel.alpha = 1f;
        }
    }

    // call in update()
    public void SetOxygen(int oxygen, int maxOxygen)
    {
        oxygenSlider.maxValue = maxOxygen;
        oxygenSlider.value = oxygen;
    }

    // call in update()
    public void SetAmmo(int ammo, int maxAmmo)
    {
        ammoText.text = ammo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
    }

    // call in update()
    public void LowValue()
    {
        lerpColor = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 0.6f));

        if (isLowHealth)
        {
            healthIcon.color = lerpColor;
            hurtPanel.alpha = 0.5f;
        }
        else
        {
            healthIcon.color = Color.white;
        }

        if (isLowOxygen)
        {
            oxygenIcon.color = lerpColor;
        }
        else
        {
            oxygenIcon.color = Color.white;
        }

        if (isLowAmmo)
        {
            ammoText.color = lerpColor;
        }
        else
        {
            ammoText.color = Color.white;
        }
    }


    public IEnumerator BossLevel()
    {
        bossWarningPanel.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        bossWarningPanel.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1.0f);
        bossWarningPanel.SetActive(false);
        bossHealthBar[0].SetActive(true);
    }

    public void SetBossHealth(int health, int maxHealth)
    {
        // for (int i = 0; i < 3; i++)
        // {
        //     bossHealthBar[i].GetComponent<Slider>().maxValue = maxHealth/3;
        //     bossHealthBar[i].GetComponent<Slider>().value = health/3;
        //     bossHealthBar[i].GetComponentInChildren<TextMeshProUGUI>().text = (health/3).ToString() + " / " + (maxHealth/3).ToString();
        //
        //     if (bossHealthBar[i].GetComponent<Slider>().value <= 0 && i != 2)
        //     {
        //         bossHealthBar[i].SetActive(false);
        //         bossHealthBar[i + 1].SetActive(true);
        //     }
        // }
        
        bossHealthBar[0].GetComponent<Slider>().maxValue = maxHealth;
        bossHealthBar[0].GetComponent<Slider>().value = health;
        bossHealthBar[0].GetComponentInChildren<TextMeshProUGUI>().text = health.ToString() + " / " + maxHealth.ToString();
        
    }

    public void SetRepairProgressBar()
    {
        repairProgressBar.SetActive(true);
        repairProgressBar.GetComponent<Slider>().maxValue = repairTime;
        repairProgressBar.GetComponent<Slider>().value = repairTime;
    }

    // call in update()
    public void RepairTimer()
    {
        float time = (repairTime + 1f) - Time.time;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60);

        string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (time <= 0)
        {   
            if (!isClose)
            {
                //结束
                SoundManager.instance.StopSFX(17);
                stopTimer = true;
                repairProgressBar.GetComponentInChildren<TextMeshProUGUI>().text = "Gate Repaired!";
                Destroy(repairProgressBar, 3f);
                GameObject.FindWithTag("Gate").GetComponent<Animator>().SetBool("isOpen",true);
                GameObject.Find("Boss").GetComponent<MonsterController>().isChasing = true;
                isBoss = true;
                //初始化血量
                
                SetBossHealth(GameObject.Find("Boss").GetComponent<MonsterController>().HP,GameObject.Find("Boss").GetComponent<MonsterController>().HP);
                isClose = true;
                
            }
            
        }

        if (!stopTimer)
        {
            repairProgressBar.GetComponentInChildren<TextMeshProUGUI>().text = "Repairing...   " + timeText;
            repairProgressBar.GetComponent<Slider>().value = Time.time;
        }
    }

    public void CollectPopup()
    {
        if (!isCollected)
        {
            collectPopup.GetComponentInChildren<Animator>().Play("CollectPopup", -1, 0f);
        }

    }
}
