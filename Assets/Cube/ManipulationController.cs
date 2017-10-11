using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ManipulationController : MonoBehaviour, IManipulationHandler
{
    Vector3 prevPos;

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

        // ???
        prevPos = eventData.CumulativeDelta;

        // need(KORE GA NAITO OBUJEKUTO NI FORCAS DEKINAI)
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        
        Vector3 moveVec = Vector3.zero;
        moveVec = eventData.CumulativeDelta - prevPos;

        prevPos = eventData.CumulativeDelta;
        

        var handDistance = 0.4f;
        var objectDistance = 
            Vector3.Distance(Camera.main.transform.position, gameObject.transform.position);

        gameObject.transform.position += (moveVec * (objectDistance / handDistance));
    }
}
