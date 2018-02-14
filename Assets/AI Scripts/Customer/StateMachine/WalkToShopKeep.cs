using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToShopKeep : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        animator.GetComponent<WanderOrShop>().walkToShopKeep();
	}

	
}
