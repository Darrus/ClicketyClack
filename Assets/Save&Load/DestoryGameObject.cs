using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryGameObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Destroy(gameObject, gameObject.GetComponentInChildren<ParticleSystem>().main.duration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
