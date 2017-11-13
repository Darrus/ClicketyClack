using UnityEngine;
using HoloToolkit.Unity.InputModule;

[RequireComponent(typeof(Rigidbody))]
public class WeightedPickUp1 : MonoBehaviour, IInputClickHandler
{
    [Range(0.0f, 8.0f)]
    public float snapDistance = 5.0f;
    public float force;

    Rigidbody rigid;
    bool pickUp = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(pickUp)
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

    public void OnInputClicked(InputClickedEventData eventData)
    {
        pickUp = !pickUp;
        rigid.useGravity = !pickUp;
    }
}
