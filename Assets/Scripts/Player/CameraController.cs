using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensity;
    
    public Transform player;

    public float rotation=0f;
    
    public static CameraController instance;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {   
        float x = Input.GetAxis("Mouse X")*mouseSensity*Time.deltaTime;
        float y = Input.GetAxis("Mouse Y")*mouseSensity*Time.deltaTime;
        rotation -= y;
        rotation = Mathf.Clamp(rotation, -90f, 30f);
        transform.localRotation = Quaternion.Euler(rotation,0f,0f);
        player.Rotate(Vector3.up*x);

    }


}
