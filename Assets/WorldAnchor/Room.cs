﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class Room : MonoBehaviour {
    public GameObject[] objectsToActivate;
    Anchor anchor;

    private void Awake()
    {
#if !UNITY_EDITOR && UNITY_WSA
    foreach(GameObject go in objectsToActivate)
    {
        go.SetActive(false);
    }
#endif
    }

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
            foreach(GameObject go in objectsToActivate)
            {
                go.SetActive(true);
            }
            Destroy(this);
        }
#endif
    }
}
