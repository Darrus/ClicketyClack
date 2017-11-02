using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class Room : Singleton<Room> {
    public bool done = false;
    //public GameObject[] objectsToActivate;
    Anchor anchor;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
#if !UNITY_EDITOR && UNITY_WSA
    //foreach(GameObject go in objectsToActivate)
    //{
    //    go.SetActive(false);
    //}
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
        if (!WorldAnchorManager.Instance.importing && !done)
        {
            anchor.LoadAnchor();

            //foreach(GameObject go in objectsToActivate)
            //{
            //    go.SetActive(true);
            //}

            Debug.Log("Room's P :" + transform.position);
            done = true;
        }
#endif

    }
}
