using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

[RequireComponent(typeof(Rigidbody))]
public class WeightedPickUp : MonoBehaviour, IManipulationHandler
{
    [Range(1.0f, 8.0f)]
    public float snapDistance = 5.0f;
    public float force;

    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.ClearModalInputStack();
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.ClearModalInputStack();
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        InputManager.Instance.ClearModalInputStack();
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        // Force = Mass * Acceleration
        // Acceleration = Change In Velocity / Time Taken
		Vector3 TargetPosition = Camera.main.transform.position + Camera.main.transform.forward * snapDistance;
        float distanceSqr = (TargetPosition - transform.position).sqrMagnitude;
        if (distanceSqr < 0.001f)
        {
            rigid.velocity = Vector3.zero;
            return;
        }

        Vector3 direction = (TargetPosition - transform.position).normalized;
        float acceleration = force / rigid.mass;
        rigid.velocity = acceleration * direction;
    }
}
