/** 
 *  @file    OnClick.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the OnClick class
 */
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity.InputModule;

/** 
 *  @brief   A hololen's class that on click will invoke Unity Events, Supports a debug button in Unity as well
 */
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

    /** 
     *  @brief  Upon hololens input click, sends an event data for the function to handle
     *  @param  eventData, an hololen's InputClickedEventData for this function to handle
     */
    public void OnInputClicked(InputClickedEventData eventData)
    {
        eventOnClick.Invoke();
    }
}
