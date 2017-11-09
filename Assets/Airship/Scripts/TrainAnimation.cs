using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAnimation : MonoBehaviour
{
    private Animation anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animation>();
        anim.Stop();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (LevelManager.CargoOn)
        {
            anim.Play();
        }
        if (LevelManager.ReachStation && LevelManager.MoveOut)
        {
            anim.Stop();
        }
    }
}
