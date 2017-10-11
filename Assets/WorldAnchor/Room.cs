using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class Room : MonoBehaviour {
    Anchor anchor;

	void Start () {
        anchor = GetComponent<Anchor>();
#if !UNITY_EDITOR && UNITY_WSA
        WorldAnchorManager.Instance.ImportFromFile();
#endif
    }

    private void Update()
    {
#if !UNITY_EDITOR && UNITY_WSA
        if (!WorldAnchorManager.Instance.importing)
        {
            anchor.LoadAnchor();
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            Destroy(this);
        }
#endif
    }
}
