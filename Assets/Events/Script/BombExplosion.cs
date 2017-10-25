using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BombExplosion : MonoBehaviour
{
    [TagSelector]
    public string triggerTag;
    public AudioSource sfx;
    public float timeTillExpload;
    public float explosionForce;
    public float explosionRadius;
    public bool triggered = false;

    private bool exploaded = false;
    private SphereCollider sphereCollider;

    public GameObject Parent;
    public GameObject ParticleEffect;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (!triggered || exploaded)
            return;

        timeTillExpload -= Time.deltaTime;
        if (timeTillExpload <= 0.0f)
        {
            exploaded = true;
            Collider[] objsWithinRadius = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider col in objsWithinRadius)
            {
                if (col.CompareTag(triggerTag))
                {
                    LevelManager.TrianConnected = false;
                    Rigidbody objRigidbody = col.GetComponent<Rigidbody>();
                    objRigidbody.useGravity = true;
                    objRigidbody.isKinematic = false;
                    objRigidbody.AddExplosionForce(explosionForce, transform.position, sphereCollider.radius);
                }
            }

            LevelManager.TrianConnected = false;
            CreateExplosion();
            sfx.gameObject.GetComponent<PlaySoundAtPosition>().PlayAtPosition();
            GameObject.Destroy(Parent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            triggered = true;
        }
    }

    private void CreateExplosion()
    {
        GameObject.Instantiate(ParticleEffect, Parent.transform.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
