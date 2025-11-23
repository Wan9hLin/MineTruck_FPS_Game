using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI;
    public GameObject WeaponCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !PlayerController.instance.isWalking)
        {
            StartCoroutine("ScopeOn");
        }
        else
        {   
            WeaponCamera.SetActive(true);
            StartCoroutine("ScopeOff");
        }
    }

    IEnumerator ScopeOn()
    {
        yield return new WaitForSeconds(0.1f);
        UI.SetActive(true);
        WeaponCamera.SetActive(false);
    }
    
    IEnumerator ScopeOff()
    {
        yield return new WaitForSeconds(0.1f);
        UI.SetActive(false);
    }
    
    
}
