using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCotroller : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;//follow camera point position
        transform.rotation = target.rotation;//follow camera point rotation
    }
}
