using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Collider))]
public class AnimateButton : MonoBehaviour, IInputClickHandler
{
    public UnityEvent onClickEvents;
    Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();        
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        myAnimator.SetTrigger("Play");
        onClickEvents.Invoke();
    }
}
