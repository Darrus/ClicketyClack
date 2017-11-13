using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ClickPickUp : MonoBehaviour, IInputClickHandler
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float ObjectDistance;
    
    private float step;

    bool pickUp = false;

    private void Update()
    {
        if(pickUp)
        {
            step = speed * Time.deltaTime;
            // Default Angle
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), step);

            GetComponent<Rigidbody>().velocity = Vector3.zero;

            gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * ObjectDistance;
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        pickUp = !pickUp;
        GetComponent<Rigidbody>().useGravity = !pickUp;
    }
}


