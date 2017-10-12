﻿using UnityEngine;
using HoloToolkit.Unity.InputModule;

[RequireComponent(typeof(Collider))]
public class PickAndSnap : MonoBehaviour, IManipulationHandler
{
    [Range(1.0f, 8.0f)]
    public float snapDistance = 5.0f;
    public float breakOffPoint = 3.0f;
    public Material placeableMaterial;
    public Material defaultMaterial;
    public MeshRenderer[] meshRenderers;
    public string snapTriggerName;

    private Collider objCollider;

    [HideInInspector]
    public bool snapped = false;
    EventBase trainEvent;

    private void Start()
    {
        objCollider = GetComponent<Collider>();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        if (snapped)
        {
            for(int i = 0; i < meshRenderers.Length; ++i)
            {
                meshRenderers[i].material = defaultMaterial;
            }
            trainEvent = null;
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        if(snapped)
        {
            for (int i = 0; i < meshRenderers.Length; ++i)
            {
                meshRenderers[i].material = defaultMaterial;
            }
            trainEvent = null;
        }
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if(!snapped)
        {
            Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * snapDistance;
            transform.position = newPosition;
        }
        else
        {
            Vector3 snapPosition = transform.position;
            Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * snapDistance;
            float sqrMagnitude = (snapPosition - newPosition).sqrMagnitude;
            for (int i = 0; i < meshRenderers.Length; ++i)
            {
                meshRenderers[i].material = placeableMaterial;
            }

            if (sqrMagnitude >= breakOffPoint * breakOffPoint)
            {
                snapped = false;
                if(trainEvent != null)
                    trainEvent.Solved = false;
                trainEvent = null;
                transform.SetParent(null);
                transform.rotation = new Quaternion();
                transform.position = newPosition;
                for (int i = 0; i < meshRenderers.Length; ++i)
                {
                    meshRenderers[i].material = defaultMaterial;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == snapTriggerName)
        {
            Transform otherTransform = other.transform;
            trainEvent = otherTransform.parent.GetComponent<EventBase>();
            if (trainEvent == null || trainEvent.Solved)
            {
                Debug.Log(trainEvent.Solved);
                return;
            }

            for (int i = 0; i < meshRenderers.Length; ++i)
            {
                meshRenderers[i].material = placeableMaterial;
            }

            transform.SetParent(otherTransform.parent);
            transform.localPosition = new Vector3();
            transform.localRotation = new Quaternion();
            snapped = true;
        }
    }
}