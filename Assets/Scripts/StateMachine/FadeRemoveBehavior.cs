using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;
    Color startColor;

    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timeElapsed = 0f;
       objToRemove = animator.gameObject;
       spriteRenderer = objToRemove.GetComponent<SpriteRenderer>();
       startColor = spriteRenderer.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timeElapsed += Time.deltaTime;
       float newAlpha = startColor.a * (1 - (timeElapsed / fadeTime));

       spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

       if(timeElapsed > fadeTime)
       {
        Destroy(objToRemove);
       }
    }
}
