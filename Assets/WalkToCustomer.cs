using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToCustomer : StateMachineBehaviour {

    FollowOrGetStockShopKeeper followOrGetStock;
    UnityEngine.AI.NavMeshAgent agent;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followOrGetStock = animator.gameObject.GetComponent<FollowOrGetStockShopKeeper>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followOrGetStock.WalkToCustomer();
    }
}
