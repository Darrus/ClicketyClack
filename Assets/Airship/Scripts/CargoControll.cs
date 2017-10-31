using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class CargoControll : MonoBehaviour, IManipulationHandler
{
    [SerializeField]
    private float speed;
    private float step;

    private void Start()
    {
        speed = 100.0f;
    }

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
        step = speed * Time.deltaTime;

        GetComponent<Rigidbody>().useGravity = false;
        InputManager.Instance.PushModalInputHandler(this.gameObject);
    }


    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        GetComponent<Rigidbody>().useGravity = true;

        // Default Angle
        this.transform.rotation =
            Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), step);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;

    }

}

