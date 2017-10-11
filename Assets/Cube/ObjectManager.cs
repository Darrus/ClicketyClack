using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ObjectManager : MonoBehaviour, IInputClickHandler
{
    public GameObject obj1;
    public GameObject obj2;
    private bool toggle = true;

    // Initialization
    private void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    // Clicked
    public void OnInputClicked(InputClickedEventData eventData)
    {
        var obj = toggle ? obj1 : obj2;
        toggle = !toggle;

        var pos = Camera.main.transform.position;
        var forward = Camera.main.transform.forward;

        Instantiate(obj, pos + forward, new Quaternion());
    }

}
