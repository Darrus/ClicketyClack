/** 
*  @file    DestoryParticleOnEndDur.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain class DestoryParticleOnEndDur
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  @brief A class to Destory Particle Effect On End its Duration
*/
public class DestoryParticleOnEndDur : MonoBehaviour {


    public GameObject Parent; ///< Gameobject of the whole current Particle Effect in

    // Use this for initialization
    void Start () {
		
	}

    /**
    *  @brief Destory Parent if Particle Effect Ended its Duration
    */
    void Update () {
        if (!gameObject.GetComponent<ParticleSystem>().IsAlive())
        {
            GameObject.Destroy(Parent);
        }
    }
}
