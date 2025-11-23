using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController2 : MonoBehaviour
{
   // public float moveSpeed;
    //public Rigidbody theRB;

    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;
    private Vector3 targetPoint, starPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    public Animator anim;
    private bool wasShot;

    public EnemyHealthController enemyHealthController;

    // Start is called before the first frame update
    void Start()
    {
        starPoint = transform.position;//store his initial position

        shootTimeCounter = timeToShoot;//shooting time 1s
        shotWaitCounter = waitBetweenShots;//waiting time(between shots) 2s 
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y; // we make the Y axis of enemy will be based on himself, not player position
        if (enemyHealthController.currentHealth > 0)
        {
            if (!chasing)
            {
                if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
                {
                    chasing = true;

                    shootTimeCounter = timeToShoot;//shooting time 1s
                    shotWaitCounter = waitBetweenShots;//waiting time(between shots) 2s 



                    // fireCount = 1f;
                }

                if (chaseCounter > 0)
                {
                    chaseCounter -= Time.deltaTime;

                    if (chaseCounter <= 0)
                    {
                        agent.destination = starPoint;
                    }
                }


            }
            else
            {
                
                if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
                {
                    agent.destination = targetPoint;
                }
                else
                {
                    agent.destination = transform.position;//enemy stops when he is close
                }



                if (Vector3.Distance(transform.position, targetPoint) > distanceToChase)
                {
                    if (!wasShot)
                    {
                        chasing = false;

                        // agent.destination = starPoint;
                        chaseCounter = keepChasingTime;
                    }

                }
                else
                {
                    wasShot = false;
                }

                if (shotWaitCounter > 0)
                {
                    shotWaitCounter -= Time.deltaTime;

                    if (shotWaitCounter <= 0)
                    {
                        shootTimeCounter = timeToShoot;
                    }

                    anim.SetBool("isMoving", true);
                }

                else if (PlayerController.instance.gameObject.activeInHierarchy)//he will attack us only when we are active in the scene
                {
                    shootTimeCounter -= Time.deltaTime;


                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;


                        if (fireCount <= 0)
                        {
                            fireCount = fireRate;

                            firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 0.5f, 0f));

                            //check the angle of the player
                            Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
                            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                            if (Mathf.Abs(angle) < 30f)
                            {
                                Instantiate(bullet, firePoint.position, firePoint.rotation);

                                anim.SetTrigger("fireShot");
                            }
                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }


                        }

                        agent.destination = transform.position;
                    }
                    else
                    {
                        shotWaitCounter = waitBetweenShots;
                    }

                    anim.SetBool("isMoving", false);
                }


            }
        }
  
    }      
    public void GetShot()
    {
        wasShot = true;

        chasing = true;

    }    
    
}
