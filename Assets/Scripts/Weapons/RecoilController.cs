using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 currentRotation;
    public Vector3 targetRotation;


    public float snappiness;
    public float returnSpeed;
    
    public static RecoilController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
    
    public void Recoil(float recoilX,float recoilY,float recoilZ)
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        
    }
}
