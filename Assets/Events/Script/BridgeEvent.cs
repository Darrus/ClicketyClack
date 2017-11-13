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

            if (other.gameObject.name == "head")
            {
                LevelManager.TheTrainLife.killHead();
            }
            if (other.gameObject.name == "carriage")
            {
                LevelManager.TheTrainLife.KillCarriage();
            }
            if (other.gameObject.name == "cargo")
            {
                LevelManager.TheTrainLife.KillCargo();
            }

        }
    }
}
