using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private float delayTime = 3.0f;
    public GameObject Parent;
    public GameObject ParticleEffect;
    public GameObject ParticleEffect2;
    public PlaySoundAtPosition explosionSFX;
    public GameObject m_target;

    void Update()
    {
        delayTime -= Time.deltaTime;

        if (delayTime < 0)
        {
            Destroy(Parent);
            Destroy(m_target);
            delayTime = 3.0f;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("FallPoint")|| col.CompareTag("Ground"))
        {
            GameObject.Instantiate(ParticleEffect, transform.position, Quaternion.identity);
            explosionSFX.PlayAtPosition();
            Destroy(Parent);
            Destroy(m_target);
        }
        if (col.gameObject.tag == "Player")
        {
            GameObject.Instantiate(ParticleEffect2, transform.position, Quaternion.identity);
            explosionSFX.PlayAtPosition();
            Destroy(Parent);
            Destroy(m_target);
        }
    }
}
