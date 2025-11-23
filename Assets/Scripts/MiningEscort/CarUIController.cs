using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUIController : MonoBehaviour
{
    public static CarUIController instance;
    public Slider healthSlider;
    private Transform Player;
    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.transform.LookAt(Player);
    }
}
