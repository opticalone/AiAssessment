using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getStockState : StateMachineBehaviour {

    WanderOrShop wos;

    FollowOrGetStockShopKeeper shopkeeep;
    
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wos = animator.GetComponent<WanderOrShop>();
        shopkeeep = wos.GetCurrentShopKeep().GetComponent<FollowOrGetStockShopKeeper>();
        if (!shopkeeep.GetShopKeepOccupied() && wos.updateBuy())
        {
            animator.SetBool("WantToBuy", true);
            animator.SetBool("shopkeepOccupied", false);
        }
        else
        {
            animator.SetBool("WantToBuy", false);
            animator.SetBool("shopkeepOccupied", true);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float dist = Vector3.Distance(wos.gameObject.transform.position, shopkeeep.gameObject.transform.position);

        animator.SetFloat("distFromShopkeep", dist);
        
        

   
    }



	
}
