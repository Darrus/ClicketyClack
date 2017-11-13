using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;

public class OnClick : MonoBehaviour, IInputClickHandler
{
    public UnityEvent eventOnClick;

#if UNITY_EDITOR
    public KeyCode debugKey;

    public void Update()
    {
        if (Input.GetKeyDown(debugKey))
            eventOnClick.Invoke();
    }
#endif

    public void OnInputClicked(InputClickedEventData eventData)
    {
        eventOnClick.Invoke();
    }
}
