using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderCheckBehaviors : StateMachineBehaviour
{
    CustomerAI custAI;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        custAI = animator.gameObject.GetComponent<CustomerAI>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (custAI.GetCurrentKeepOccupied())
            animator.SetBool("keepOccupied", true);
        else
            animator.SetBool("keepOccupied", false);
    }
}
