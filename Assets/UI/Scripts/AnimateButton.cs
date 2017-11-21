/** 
 *  @file     AnimateButton.cs
 *  @author Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the Animate Button class, it requires Animator and Collider class to work with
 */
 using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.Events;

/** 
 *  @brief   Animate Button, using Hololens Input Click, it'll trigger the animator to play the animation
 */
[RequireComponent(typeof(Animator), typeof(Collider))]
public class AnimateButton : MonoBehaviour, IInputClickHandler
{
    public UnityEvent onClickEvents;
    Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();        
    }

    /** 
      *  @brief   Upon input clicked, it'll trigger the animator to play the animation
      *  @param  eventData, hololen's parameter for data on the input clicked
      */
    public void OnInputClicked(InputClickedEventData eventData)
    {
        myAnimator.SetTrigger("Play");
        onClickEvents.Invoke();
    }
}
