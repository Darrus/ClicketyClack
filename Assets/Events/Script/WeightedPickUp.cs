using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

[RequireComponent(typeof(Rigidbody))]
public class WeightedPickUp : MonoBehaviour, IManipulationHandler
{
    [Range(1.0f, 8.0f)]
    public float snapDistance = 5.0f;
    public float speed;

    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.ClearModalInputStack();
        rigid.useGravity = true;
        rigid.isKinematic = false;
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.ClearModalInputStack();
        rigid.useGravity = true;
        rigid.isKinematic = false;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        InputManager.Instance.ClearModalInputStack();
        InputManager.Instance.PushModalInputHandler(gameObject);
        rigid.useGravity = false;
        rigid.isKinematic = true;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        Vector3 TargetPosition = Camera.main.transform.position + Camera.main.transform.forward * snapDistance;
        Vector3 direction = (TargetPosition - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * (speed / rigid.mass);
        transform.position = newPosition;
    }
}
