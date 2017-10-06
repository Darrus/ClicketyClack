using UnityEngine;
using HoloToolkit.Unity;

using UnityEngine.VR.WSA;

public class Anchor : MonoBehaviour
{
    public string objectAnchorStoreName;

    public void SaveAnchor()
    {
        if (WorldAnchorManager.Instance.CheckAnchorExist(objectAnchorStoreName))
            WorldAnchorManager.Instance.RemoveAnchorFromStore(objectAnchorStoreName);
        LoadAnchor();
    }

    public void LoadAnchor()
    {
        WorldAnchorManager.Instance.AttachAnchor(this.gameObject, objectAnchorStoreName);
    }

    public void RemoveAnchor()
    {
        WorldAnchorManager.Instance.RemoveAnchor(this.gameObject);
    }

    public void ExportAnchor()
    {
        if(this.GetComponent<WorldAnchor>() == null)
        {
            Debug.LogError("Anchor does not exist, Please save before exporting.");
            return;
        }

        WorldAnchorManager.Instance.ExportToFile(this.gameObject);
    }
}
