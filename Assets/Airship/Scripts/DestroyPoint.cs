using UnityEngine;

public class DestroyPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}

