using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;
    public bool enabled = true;

    // Update is called once per frame
    void Update()
    {
        // If movement is enabled, update the object's position and rotation along the path
        if (enabled)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

        }
    }

}
