using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounsPad : MonoBehaviour
{

    public float bounceForce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySFX(0); 
            PlayeController.instance.Bounce(bounceForce);
        }
    }
}
