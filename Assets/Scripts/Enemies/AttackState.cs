using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AttackState : StateMachineBehaviour
{
    Transform player;
    public int damage;

    public float timer;

    public float attackRate = 1f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer < attackRate)
        {
            timer += Time.deltaTime;
        }
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(player.position, animator.transform.position);
       
        if (distance > 3.5f)
        {
            animator.SetBool("isAttacking", false);
            
        }

        // new change
        else if(distance < 2.5f)
        {   
            if(timer<attackRate)return;
            //伤害计算
            PlayerHpController.instance.TakeDanmage(damage);
           // CarHealthController.instance.DamageEnemy(damage);
            animator.SetTrigger("attack");
            SoundManager.instance.PlaySFX(12);
            timer = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
