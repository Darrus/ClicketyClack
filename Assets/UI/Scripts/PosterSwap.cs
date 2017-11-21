/** 
 *  @file     PosterSwap.cs
 *  @author Darrus
 *  @date    21/11/2017  
 *  @brief   Contains the poster swap class
 */
using UnityEngine;
using HoloToolkit.Unity.InputModule;

/** 
 *  @brief   Swap the target renderer with an array of materials provided
 */
[RequireComponent(typeof(ReversePlayAnimation))]
public class PosterSwap : MonoBehaviour {
    [SerializeField]
    HandDraggablePluck targetPluck;

    [SerializeField]
    Renderer targetMesh;

    public Material[] materials;
    ReversePlayAnimation controller;

    private void Awake()
    {
        controller = GetComponent<ReversePlayAnimation>();
    }

    /** 
      *  @brief   When the controller has finished playing the reversed animation, set the targetMesh material to a 
      *                random material of the array of materials provided.
      */
    private void Update()
    {
        if(controller.previousState == ReversePlayAnimation.ANIM_STATE.REVERSE && controller.State == ReversePlayAnimation.ANIM_STATE.REVERSED && !targetPluck.isPlucked)
        {
            targetMesh.material = materials[Random.Range(0, materials.Length)];        
        }
    }
}
