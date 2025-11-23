using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class storgeValue : MonoBehaviour
{
    public Slider slider;
    public MineManager mineManager;
    Transform Player;
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.LookAt(Player);
        if (GameManager.instance.counter == 20)
        {
            GameManager.instance.WinPanel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && GameManager.instance.counter !=0)
        {
            slider.value += GameManager.instance.counter;
            GameManager.instance.counter = 0;
            mineManager.MineLabel.text = GameManager.instance.counter + "/3";
            SoundManager.instance.PlaySFX(11);
        }
    }
}
