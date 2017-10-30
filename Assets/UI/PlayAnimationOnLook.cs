using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Animator))]
public class PlayAnimationOnLook : MonoBehaviour
{
    enum ANIM_STATE
    {
        PLAY,
        REVERSE
    }
    ANIM_STATE state = ANIM_STATE.REVERSE;
    public float lookDistance;

    Collider col;
    Camera mainCam;
    Animator anim;

    private void Awake()
    {
        mainCam = Camera.main;
        col = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit);

        AnimatorStateInfo animationState = anim.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] myAnimatorClip = anim.GetCurrentAnimatorClipInfo(0);
        float myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;

        switch(state)
        {
            case ANIM_STATE.PLAY:
                anim.SetFloat("speedMultiplier", 1.0f);
                if(myTime >= myAnimatorClip[0].clip.length)
                {
                     anim.SetFloat("speedMultiplier", 0.0f);
                }
                if (hit.transform == null || hit.distance >= lookDistance || hit.collider != col)
                {
                    state = ANIM_STATE.REVERSE;
                }
                break;
            case ANIM_STATE.REVERSE:
                anim.SetFloat("speedMultiplier", -1.0f);
                if (myTime <= 0.0f)
                {
                     anim.SetFloat("speedMultiplier", 0.0f);
                }
                if (hit.transform != null && hit.distance <= lookDistance && hit.collider == col)
                {
                    state = ANIM_STATE.PLAY;
                }
                break;
        }
    }
}
