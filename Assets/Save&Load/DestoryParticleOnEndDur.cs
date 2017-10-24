using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryParticleOnEndDur : MonoBehaviour {


    public GameObject Parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameObject.GetComponent<ParticleSystem>().IsAlive())
        {
            GameObject.Destroy(Parent);
        }
    }
}
