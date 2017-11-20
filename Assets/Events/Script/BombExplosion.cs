/** 
 *  @file    BombExplosion.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Bomb explosion handler
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 *  @brief   Handles the explosion of the bomb towards the affected target
 */
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

    /** 
     *  @brief   Get the Sphere Collider from this game object
     */
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    /** 
     *  @brief   When the bomb is triggered, it counts down until it exploads
     */
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
                    if(col.gameObject.name == "head")
                    {
                        GameBoard.Instance.TheTrainLife.killHead();
                    }
                    if (col.gameObject.name == "carriage")
                    {
                        GameBoard.Instance.TheTrainLife.KillCarriage();
                    }
                    if (col.gameObject.name == "cargo")
                    {
                        GameBoard.Instance.TheTrainLife.KillCargo();
                    }

                    Rigidbody objRigidbody = col.GetComponent<Rigidbody>();
                        objRigidbody.useGravity = true;
                        objRigidbody.isKinematic = false;
                        objRigidbody.AddExplosionForce(explosionForce, transform.position, sphereCollider.radius);
               
                }
            }
            CreateExplosion();
            sfx.gameObject.GetComponent<PlaySoundAtPosition>().PlayAtPosition();
            GameObject.Destroy(Parent);
        }
    }

    /** 
     *  @brief   Sets the boolean to true when it runs into the collided tag
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            triggered = true;
        }
    }

    /** 
     *  @brief   Creates the particle effect of the explosion
     */
    private void CreateExplosion()
    {
        GameObject.Instantiate(ParticleEffect, Parent.transform.position, Quaternion.identity);
    }

    /** 
     *  @brief   Draws the explosion radius
     */
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
