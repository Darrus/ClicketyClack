using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ManipulationController : MonoBehaviour, IManipulationHandler
{
    [SerializeField]
    private float speed = 100.0f;

    [SerializeField]
    private float ObjectDistance;
    
    private float step;

    
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        GetComponent<Rigidbody>().useGravity = true;

        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        GetComponent<Rigidbody>().useGravity = true;
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        GetComponent<Rigidbody>().useGravity = false;

        // Back to Default Angle
        step = speed * Time.deltaTime;

        // need(KORE GA NAITO OBUJEKUTO NI FORCAS DEKINAI)
        InputManager.Instance.PushModalInputHandler(this.gameObject);
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        GetComponent<Rigidbody>().useGravity = true;

        // Default Angle
        this.transform.rotation =
            Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), step);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
       gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * ObjectDistance;

    }

}


