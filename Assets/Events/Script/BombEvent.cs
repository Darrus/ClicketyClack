using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEvent : EventBase {
    [TagSelector]
    public string playerTag;
    public Collider bombCollider;

    private void Awake()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag(playerTag);
        foreach(GameObject player in playerObjects)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), bombCollider);
        }
    }
}
