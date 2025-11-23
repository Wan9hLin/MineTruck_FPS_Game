using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText, ammoText;

    public Image damageEffect;
    public float damageAlpha = .25f, damageFadeSpeed = 2f;

    public GameObject pauseScreen;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { 
    
    
    }
    
    

}
