using UnityEngine;
using HoloToolkit.Unity.InputModule;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Pluck1 : MonoBehaviour, IInputClickHandler
{
    [Range(0.0f, 8.0f)]
    public float snapDistance = 5.0f;

    public float force;

    Animator myAnimator;
    Rigidbody myRigidbody;
    public bool plucked = false;

    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (plucked)
        {
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * snapDistance;
            float distanceSqr = (targetPosition - transform.position).sqrMagnitude;
            if (distanceSqr < 0.001f)
            {
                myRigidbody.velocity = Vector3.zero;
                return;
            }

            Vector3 direction = (targetPosition - transform.position).normalized;
            float acceleration = force / myRigidbody.mass;
            myRigidbody.velocity = acceleration * direction;
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        plucked = !plucked;
        myAnimator.SetTrigger("tear");
        myRigidbody.useGravity = !plucked;
    }
}
