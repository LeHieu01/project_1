using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAfterDeathBehaviour : StateMachineBehaviour
{
    public float fadeOutTime = 1f;
    private float timePassed = 0f;
    SpriteRenderer SpriteRenderer;
    Color originalColor;
    GameObject gameObject;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed = 0f;
        SpriteRenderer = animator.GetComponent<SpriteRenderer>();
        originalColor = SpriteRenderer.color;
        gameObject = animator.gameObject;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed += Time.deltaTime;

        float opacity = originalColor.a * (1 - (timePassed / fadeOutTime));

        SpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, opacity);
        if (timePassed > fadeOutTime)
        {
            Destroy(this.gameObject);
        }

    }



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
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
