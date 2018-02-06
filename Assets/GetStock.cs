using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStock : StateMachineBehaviour
{
    FollowOrGetStockShopKeeper followOrGetStock;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followOrGetStock = animator.gameObject.GetComponent<FollowOrGetStockShopKeeper>();
        followOrGetStock.GetStock();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followOrGetStock.GetStock();
    }
}
