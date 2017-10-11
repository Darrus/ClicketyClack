using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class PolyTester : MonoBehaviour, IManipulationHandler {
    public TextMesh textMesh;
    public float scaleSpeed = 10.0f;
    public const int maxRange = 100;

    [Range(1, maxRange)]
    public float scaleFactor = 1.0f;

    private void Start()
    {
    }

#if UNITY_EDITOR && !UNITY_WSA
    private void Update()
    {
        if (Input.GetKey(KeyCode.Equals))
            Scale(1.0f);
        if (Input.GetKey(KeyCode.Minus))
            Scale(-1.0f);
    }
#endif

    void Scale(float scaleDelta)
    {
        scaleFactor += scaleSpeed * scaleDelta * Time.deltaTime;
        if (scaleFactor > maxRange)
            scaleFactor = maxRange;
        else if (scaleFactor < 1)
            scaleFactor = 1;

        textMesh.text = "Scale Factor : " + scaleFactor.ToString();
        this.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        float axisDelta = eventData.CumulativeDelta.x;
        Scale(axisDelta);
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
    }
}
