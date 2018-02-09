using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCustomer : StateMachineBehaviour
{
    FollowOrGetStockShopKeeper followOrGetStock;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FollowOrGetStockShopKeeper followOrGetStock = animator.gameObject.GetComponent<FollowOrGetStockShopKeeper>();
        followOrGetStock.Follow();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FollowOrGetStockShopKeeper followOrGetStock = animator.gameObject.GetComponent<FollowOrGetStockShopKeeper>();
        followOrGetStock.Follow();
    }
}
