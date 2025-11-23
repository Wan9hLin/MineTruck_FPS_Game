using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MineManager : MonoBehaviour
{
    public static MineManager instance;
    public TextMeshProUGUI MineLabel;
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Refresh()
    {
      
            GameManager.instance.counter += 1;
            MineLabel.text = GameManager.instance.counter + "/3";
        
    }
}
