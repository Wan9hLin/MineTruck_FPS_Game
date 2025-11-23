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
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (enabled)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

        }
    }



}
