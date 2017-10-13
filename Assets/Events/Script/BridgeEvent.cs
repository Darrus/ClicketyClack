using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BridgeEvent : EventBase {
    [TagSelector]
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (Solved)
            return;

        if(other.CompareTag(playerTag))
        {
            Rigidbody rigid = other.transform.GetComponent<Rigidbody>();
            rigid.useGravity = true;
            rigid.isKinematic = false;
            LevelManager.TrianConnected = false;
        }
    }
}
