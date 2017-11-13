using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerTrainDeath : MonoBehaviour {
    [TagSelector]
    public string triggerTag;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(triggerTag))
        {

            if (collision.gameObject.name == "head")
            {
                LevelManager.TheTrainLife.killHead();
            }
            if (collision.gameObject.name == "carriage")
            {
                LevelManager.TheTrainLife.KillCarriage();
            }
            if (collision.gameObject.name == "cargo")
            {
                LevelManager.TheTrainLife.KillCargo();
            }

            Rigidbody rigid = collision.transform.GetComponent<Rigidbody>();
            if (!rigid)
                return;

            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }
}
