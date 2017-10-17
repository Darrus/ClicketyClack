using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BombExplosion : MonoBehaviour {
    [TagSelector]
    public string triggerTag;
    public float timeTillExpload;
    public float explosionForce;
    public bool triggered = false;

    private bool exploaded = false;
    private SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (!triggered || exploaded)
            return;

        timeTillExpload -= Time.deltaTime;
        if(timeTillExpload <= 0.0f)
        {
            exploaded = true;
            Collider[] objsWithinRadius = Physics.OverlapSphere(transform.position, sphereCollider.radius);
            foreach(Collider col in objsWithinRadius)
            {
                if(col.CompareTag(triggerTag))
                {
                    LevelManager.TrianConnected = false;
                    Rigidbody objRigidbody = col.GetComponent<Rigidbody>();
                    objRigidbody.useGravity = true;
                    objRigidbody.isKinematic = false;
                    objRigidbody.AddExplosionForce(explosionForce, transform.position, sphereCollider.radius);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(triggerTag))
        {
            triggered = true;
        }
    }
}
