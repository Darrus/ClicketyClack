using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerTrainDeath : MonoBehaviour {
    [TagSelector]
    public string triggerTag;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(triggerTag))
        {
            GameObject[] Temp;
            Temp = GameObject.FindGameObjectsWithTag("Player");

            LevelManager.TrianConnected = false;
            Debug.Log("DEATH");
            

            foreach (GameObject temp in Temp)
            {
                Rigidbody rigid = temp.transform.GetComponent<Rigidbody>();
                rigid.useGravity = true;
                rigid.isKinematic = false;
            }
        }
    }
}
