using UnityEngine;
using HoloToolkit.Unity.InputModule;

[RequireComponent(typeof(Collider))]
public class PickUpAndSnap : MonoBehaviour, IManipulationHandler
{
    [Range(1.0f, 8.0f)]
    public float snapDistance = 5.0f;
    public float breakOffPoint = 3.0f;
    public Material placeableMaterial;
    public Material defaultMaterial;
    public MeshRenderer[] meshRenderers;

    private const string mountTagName = "MountPoint";
    private Collider objCollider;

    [HideInInspector]
    public bool snapped = false;

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
        if (other.CompareTag(mountTagName))
        {
            Vector3 newPosition;
            switch (other.name)
            {
                case "leftPoint":
                    snapped = true;
                    newPosition = new Vector3(other.transform.localPosition.x - objCollider.bounds.size.x * 0.5f, other.transform.localPosition.y, other.transform.localPosition.z);
                    transform.SetParent(other.transform.parent);
                    transform.localRotation = new Quaternion();
                    transform.localPosition = newPosition;
                    break;
                case "rightPoint":
                    snapped = true;
                    newPosition = new Vector3(other.transform.localPosition.x + objCollider.bounds.size.x * 0.5f, other.transform.localPosition.y, other.transform.localPosition.z);
                    transform.SetParent(other.transform.parent);
                    transform.localRotation = new Quaternion();
                    transform.localPosition = newPosition;
                    break;
                case "frontPoint":
                    snapped = true;
                    newPosition = new Vector3(other.transform.localPosition.x + objCollider.bounds.size.z * 0.5f, other.transform.localPosition.y, other.transform.localPosition.z);
                    transform.SetParent(other.transform.parent);
                    transform.localRotation = new Quaternion();
                    transform.localPosition = newPosition;
                    break;
                case "backPoint":
                    snapped = true;
                    newPosition = new Vector3(other.transform.localPosition.x - objCollider.bounds.size.z * 0.5f, other.transform.localPosition.y, other.transform.localPosition.z);
                    transform.SetParent(other.transform.parent);
                    transform.localRotation = new Quaternion();
                    transform.localPosition = newPosition;
                    break;
                default:
                    Debug.Log("Error mount point in " + other.transform.parent.name + ", name is incorrect.");
                    break;
            }
        }
    }
}
