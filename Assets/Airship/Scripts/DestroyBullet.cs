using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField]
    PlaySoundAtPosition m_ExploadingSFX;

    private float delayTime = 3.0f;
    bool destroy = false;

    void Update()
    {
        delayTime -= Time.deltaTime;

        if (delayTime < 0)
        {
            Destroy(this.transform.parent.gameObject);

            delayTime = 3.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FallPoint" || other.gameObject.tag == "Ground")
        {
            m_ExploadingSFX.PlayAtPosition();
            Destroy(this.transform.parent.gameObject);
        }
    }
}
