using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToKeep : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CustomerAI cust = animator.gameObject.GetComponent<CustomerAI>();
        animator.GetComponent<NavMeshAgent>().SetDestination(cust.GetCurrentShopkeep().transform.position);
    }
}
