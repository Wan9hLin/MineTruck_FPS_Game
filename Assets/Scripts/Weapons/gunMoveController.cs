using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunMoveController : MonoBehaviour
{
    public float shakeMovementAmount;

    public float smoothAmount;

    public float maxMoveAmount;

    public Vector3 originGunPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        originGunPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        viewMoveGunSwag();
    }

    public void viewMoveGunSwag()
    {   
        //惯性向相反方向
        float mX = -Input.GetAxis("Mouse X") * shakeMovementAmount;
        float mY = -Input.GetAxis("Mouse Y") * shakeMovementAmount;

        mX = Mathf.Clamp(mX, -maxMoveAmount, maxMoveAmount);
        mY = Mathf.Clamp(mY, -maxMoveAmount, maxMoveAmount);

        Vector3 moveToPositon = new Vector3(mX, mY, 0);
        
        transform.localPosition = Vector3.Lerp(transform.localPosition,moveToPositon+originGunPosition,Time.deltaTime * smoothAmount);
    }
}
