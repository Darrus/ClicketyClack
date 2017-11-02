using UnityEngine;
using HoloToolkit.Unity.InputModule;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Pluck : MonoBehaviour, INavigationHandler
{
    public int animStateOnPluck = 0;

    [Range(0.0f, 1.0f)]
    public float breakOffPoint;

    [Range(0.0f, 8.0f)]
    public float snapDistance = 5.0f;

    public float force;

    private Animator myAnimator;
    Rigidbody myRigidbody;
    public bool plucked = false;

    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }

    public void OnNavigationCanceled(NavigationEventData eventData)
    {
        InputManager.Instance.ClearModalInputStack();
    }

    public void OnNavigationCompleted(NavigationEventData eventData)
    {
        InputManager.Instance.ClearModalInputStack();
    }

    public void OnNavigationStarted(NavigationEventData eventData)
    {
        InputManager.Instance.ClearModalInputStack();
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    public void OnNavigationUpdated(NavigationEventData eventData)
    {
        if(!plucked)
        {
            if (eventData.CumulativeDelta.sqrMagnitude >= breakOffPoint * breakOffPoint)
            {
                myAnimator.SetInteger("Choice", animStateOnPluck);
                plucked = true;
            }
        }
        else
        {
            myRigidbody.useGravity = true;
        }
        //else
        //{
        //    Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * snapDistance;
        //    float distanceSqr = (targetPosition - transform.position).sqrMagnitude;
        //    if (distanceSqr < 0.001f)
        //    {
        //        myRigidbody.velocity = Vector3.zero;
        //        return;
        //    }

        //    Vector3 direction = (targetPosition - transform.position).normalized;
        //    float acceleration = force / myRigidbody.mass;
        //    myRigidbody.velocity = acceleration * direction;
        //}
    }
}
