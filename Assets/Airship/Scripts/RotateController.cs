using System;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class RotateController : MonoBehaviour,IInputHandler,ISourceStateHandler
{
    private bool isHold;
    private IInputSource currentInputSource;
    private uint currentInputSourceId;
    private Vector3 prevPos;
    private GameObject targetObj;

    // Initialization
    void Start()
    {
        targetObj = transform.root.gameObject;

        //targetObj = this.gameObject;
    }

    // Update
    void Update()
    {
        if (!isHold) return;

        Vector3 handPos;

        currentInputSource.TryGetPosition(currentInputSourceId, out handPos);

        // WorldPos -> LocalPos
        handPos = Camera.main.transform.InverseTransformDirection(handPos);

        var diff = prevPos - handPos;
        prevPos = handPos;

        // Rotate
        targetObj.transform.Rotate(0f, diff.x * 360, 0f, Space.World);
    }

    // IInputHandler---------------------------------------
    public void OnInputUp(InputEventData eventData)
    {
        if (!isHold) return;

        isHold = false;
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnInputDown(InputEventData eventData)
    {
        if (!eventData.InputSource.SupportsInputInfo
    (eventData.SourceId, SupportedInputInfo.Position)) return;

        if (isHold) return;

        isHold = true;

        InputManager.Instance.PushModalInputHandler(gameObject);

        currentInputSource = eventData.InputSource;
        currentInputSourceId = eventData.SourceId;

        currentInputSource.TryGetPosition(currentInputSourceId, out prevPos);
        prevPos = Camera.main.transform.InverseTransformDirection(prevPos);

        Debug.Log("RotateHandle");

    }



    // ISourceStateHandler----------------------------------
    public void OnSourceDetected(SourceStateEventData eventData)
    {
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        if (!isHold) return;

        isHold = false;
        InputManager.Instance.PopModalInputHandler();
    }


}
