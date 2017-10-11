using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class PolySpawner : MonoBehaviour, IInputClickHandler {
    public int objectCount;
    public GameObject prefab;
    public TextMesh textMesh;

    private void Start()
    {
        InputManager.Instance.AddGlobalListener(this.gameObject);
    }

    public void OnInputClicked(InputClickedEventData clickEvent)
    {
        objectCount++;
        GameObject objectSpawned = Instantiate<GameObject>(prefab);
        objectSpawned.transform.SetParent(this.transform);
        textMesh.text = objectCount.ToString();
    }
}
