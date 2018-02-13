using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPatrolPointState : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GuardAI guardAI = animator.gameObject.GetComponent<GuardAI>();
        guardAI.SetNextPatrolPoint();
    }


}
