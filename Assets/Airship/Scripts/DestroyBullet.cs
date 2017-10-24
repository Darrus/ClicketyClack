using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private float delayTime = 3.0f;
    public GameObject Parent;
    public GameObject ParticleEffect;
    public GameObject ParticleEffect2;

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
            GameObject.Instantiate(ParticleEffect, transform.position, Quaternion.identity);
            Destroy(Parent);
        }
        if (col.gameObject.tag == "Player")
        {
            GameObject.Instantiate(ParticleEffect2, transform.position, Quaternion.identity);
        }
    }
}
