using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class SphereManager : MonoBehaviour, IInputClickHandler
{

    // Initialization
    private void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(this.gameObject);
    }

    // Clicked
    public void OnInputClicked(InputClickedEventData eventData)
    {
        var rigid = GetComponent<Rigidbody>();
        rigid.useGravity = true;
    }

}
