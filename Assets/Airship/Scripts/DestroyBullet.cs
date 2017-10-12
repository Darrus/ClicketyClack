using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private float delayTime = 3.0f;

    void Update()
    {
        delayTime -= Time.deltaTime;

        if (delayTime < 0)
        {
            Destroy(this.gameObject);

            delayTime = 3.0f;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "FallPoint" || col.gameObject.tag == "Ground")
        {

            Destroy(this.gameObject);            
        }    
    }
}
