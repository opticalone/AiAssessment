using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitForStock : StateMachineBehaviour
{
    CustomerAI custAI;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        custAI = animator.gameObject.GetComponent<CustomerAI>();
        custAI.Wait();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!custAI.KeepGettingStock())
        {
            animator.SetBool("receivedStock", true);
        }
    }
}
