using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour
{
    public Slider slider;
    public float FillSpeed = 0.5f;
    public float targretProgress = 0;
    public bool isEnable = false;
    public GameObject Mines;
    public Detecting detecting;
   

   
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        IncrementProgress(50f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnable && GameManager.instance.counter < 3)
        {
            if (slider.value < targretProgress)
            {
                if (Input.GetKey(KeyCode.G))
                {
                    slider.value += FillSpeed * Time.deltaTime;
                    PlayMineSound();


                }

                else
                {   
                    SoundManager.instance.StopSFX(10);
                    SoundManager.instance.isplaying = false;
                }
            }

            else
            {
                detecting.MineSlider.SetActive(false);
                Invoke("MineDes", 1);
                PackAdd();

                SoundManager.instance.StopSFX(10);
                SoundManager.instance.isplaying = false;

            }
          

            

        }
    }

    public void IncrementProgress(float newProgress)
    {
        targretProgress = slider.value + newProgress;
    }

    public void MineDes()
    {
        Destroy(Mines);
        isEnable = false;
        slider.value = 0;


    }
    public void PackAdd()
    {
        MineManager.instance.Refresh();
    }

    public void GetCurrentMineObj(GameObject mine)
    {
        Mines = mine;
    }
    public void PlayMineSound()
    {
        if (!SoundManager.instance.isplaying)
        {
            SoundManager.instance.PlaySFX(10);
        }
    }
}
