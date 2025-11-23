using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject bullet;

    public float rangeToTargetPlayer, timeBetweenShots = .5f;
    private float shotCounter;

    public Transform gun, firepoint;

    public float rotateSpeed = 45f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayeController.instance.transform.position) < rangeToTargetPlayer)
        {
            gun.LookAt(PlayeController.instance.transform.position);

            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                Instantiate(bullet, firepoint.position, firepoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }

        else
        {
            shotCounter = timeBetweenShots;
            gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.Euler(0f, gun.rotation.eulerAngles.y + 10f, 0f), rotateSpeed * Time.deltaTime);
        }
    }
}
