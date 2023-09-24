using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAttacking : StateMachineBehaviour
{
    //Den h�r anv�nds f�r att f� spelaren att sluta attackera och kunna r�ra sig igen.

    //P� spelarens Idle i animatorn, l�gg till det h�r scriptet som en behaviour. Animatorn var lite konstig f�r mig s� att det bara fungerade om man skapade ett nytt script genom att trycka add behaviour och d�per det till just "StopAttacking" och inte kunde flytta den till en annan folder �n d�r den hamnade

    //Ge en referens till PlayerMovement, d�r attack funktionerna ocks� borde vara.

  private PlayerMovement playerMovement;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerIndex)
    {
        playerMovement = animator.GetComponent<PlayerMovement>();
       playerMovement.isAttacking = false;
    }





    //Ignorera allt under detta, det kom automagiskt n�r man gjorde en statemachinebehaviour.


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
