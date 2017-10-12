using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerTrainDeath : MonoBehaviour {
    [TagSelector]
    public string triggerTag;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(triggerTag))
        {
            Debug.Log("DEATH");
            LevelManager.TrianConnected = false;
            Rigidbody rigid = collision.transform.GetComponent<Rigidbody>();
            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }
}
