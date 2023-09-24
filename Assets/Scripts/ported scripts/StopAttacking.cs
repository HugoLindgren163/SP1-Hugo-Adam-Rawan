using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAttacking : StateMachineBehaviour
{
    //Den här används för att få spelaren att sluta attackera och kunna röra sig igen.

    //På spelarens Idle i animatorn, lägg till det här scriptet som en behaviour. Animatorn var lite konstig för mig så att det bara fungerade om man skapade ett nytt script genom att trycka add behaviour och döper det till just "StopAttacking" och inte kunde flytta den till en annan folder än där den hamnade

    //Ge en referens till PlayerMovement, där attack funktionerna också borde vara.

  private PlayerMovement playerMovement;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerIndex)
    {
        playerMovement = animator.GetComponent<PlayerMovement>();
       playerMovement.isAttacking = false;
    }





    //Ignorera allt under detta, det kom automagiskt när man gjorde en statemachinebehaviour.


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
