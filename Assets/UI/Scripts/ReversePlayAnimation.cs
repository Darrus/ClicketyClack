/** 
 *  @file     ReversePlayAnimation.cs
 *  @author Darrus
 *  @date    21/11/2017  
 *  @brief   Contains the reverse play animation class
 */
using UnityEngine;

/** 
 *  @brief   Play and Reverse state controller
 */
[RequireComponent(typeof(Animator))]
public class ReversePlayAnimation : MonoBehaviour {

    public enum ANIM_STATE
    {
        PLAYED,
        REVERSED,
        PLAY,
        REVERSE
    }
    ANIM_STATE state = ANIM_STATE.REVERSED;
    ANIM_STATE prevState;

    public ANIM_STATE State
    {
        get
        {
            return state;
        }
    }

    public ANIM_STATE previousState
    {
        get
        {
            return prevState;
        }
    }

    Animator myAnimator;
    AnimatorStateInfo animatorState;
    AnimatorClipInfo[] animatorClip;
    float animTime;

    private void Awake()
    {
        prevState = state;
        myAnimator = GetComponent<Animator>();
    }

    /** 
      *  @brief   Update each states of the animation
      */
    private void Update()
    {
        animatorState = myAnimator.GetCurrentAnimatorStateInfo(0);
        animatorClip = myAnimator.GetCurrentAnimatorClipInfo(0);
        float animTime = animatorClip[0].clip.length * animatorState.normalizedTime;

        switch (state)
        {
            case ANIM_STATE.PLAY:
                myAnimator.SetFloat("speedMultiplier", 1.0f);
                if (animTime >= animatorClip[0].clip.length)
                {
                    prevState = state;
                    state = ANIM_STATE.PLAYED;
                }
                break;
            case ANIM_STATE.REVERSE:
                myAnimator.SetFloat("speedMultiplier", -1.0f);
                if (animTime <= 0.0f)
                {
                    prevState = state;
                    state = ANIM_STATE.REVERSED;
                }
                break;
            default:
                prevState = state;
                myAnimator.SetFloat("speedMultiplier", 0.0f);
                break;
        }
    }

    /** 
      *  @brief   Plays the animation as per normal
      */
    public void PlayAnimation()
    {
        prevState = state;
        state = ANIM_STATE.PLAY;
    }

    /** 
      *  @brief   Reverse play the animation
      */
    public void ReverseAnimation()
    {
        prevState = state;
        state = ANIM_STATE.REVERSE;
    }

    /** 
      *  @brief   Stop the animation
      */
    public void StopAnimation()
    {
        prevState = state;
        state = ANIM_STATE.REVERSED;
    }
}
