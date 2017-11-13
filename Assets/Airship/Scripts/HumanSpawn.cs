using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;


public class HumanSpawn : MonoBehaviour, IInputClickHandler
{
    public GameObject[] human;

    private void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        int rand = Random.Range(1, 3);
        Vector3 CameraForward = Camera.main.transform.position + Camera.main.transform.forward;
        Instantiate(human[rand], CameraForward, new Quaternion());
    }
}