using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f,player.eulerAngles.y,0f);
    }
}
