using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : StateMachineBehaviour
{
    //Transform parent;
    //[SerializeField] int clipIndex;
    //float maxMovement = 1.5f;

    //Vector3 target = Vector3.zero;
    //float positionOffset = 0;
    //Vector3 velocity = Vector3.zero;

    //// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    //currentClip = clipIndex;
    //    parent = animator.gameObject.transform;
    //    switch(clipIndex)
    //    {
    //        case 0:
    //            target = parent.position + new Vector3(-maxMovement, 0, 0);
    //            break;
    //        case 1:
    //            target = parent.position + new Vector3(maxMovement, 0, 0);
    //            break;
    //        case 2:
    //            target = parent.position + new Vector3(maxMovement, 0, 0);
    //            break;
    //        case 3:
    //            target = parent.position + new Vector3(-maxMovement, 0, 0);
    //            break;
    //    }
    //}

    //// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    parent.position = Vector3.SmoothDamp(parent.position, target, ref velocity, 0.1f);

    //    switch (clipIndex)
    //    {
    //        case 0:
    //            // peak left
                
                


    //            break;
    //        case 1:
    //            //peak left reverse
                

    //            break;
    //        case 2:
    //            //peak right

    //            break;
    //        case 3:
    //            //peak right reverse
                
    //            break;
    //    }
    //}
    //IEnumerator MoveLeft(float endValue)
    //{
    //    while (positionOffset > endValue)
    //    {
    //        positionOffset = Mathf.Lerp(positionOffset, endValue, 0.1f);
    //        parent.position += new Vector3(positionOffset, 0, 0);
    //        yield return null;
    //    }
    //}
    //IEnumerator MoveRight(float startValue, float endValue)
    //{
    //    while (positionOffset < endValue)
    //    {
    //        positionOffset = Mathf.Lerp(positionOffset, endValue, 0.1f);
    //        parent.position += new Vector3(positionOffset, 0, 0);
    //        yield return null;
    //    }

    //}


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    parent.position.x = Mathf.Clamp(parent.position.x, parent.position.x - maxMovement, parent.position.x + maxMovement);

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
