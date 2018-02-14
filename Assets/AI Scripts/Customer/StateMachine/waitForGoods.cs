using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitForGoods : StateMachineBehaviour {
    WanderOrShop wos;
    FollowOrGetStockShopKeeper shopkeeep;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        wos = animator.gameObject.GetComponent<WanderOrShop>();
        
        shopkeeep = wos.GetCurrentShopKeep().GetComponent<FollowOrGetStockShopKeeper>();

        wos.waitForGoods();

        
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float dist = Vector3.Distance(wos.gameObject.transform.position, shopkeeep.gameObject.transform.position);

        animator.SetFloat("distFromShopkeep", dist);




    }
}
