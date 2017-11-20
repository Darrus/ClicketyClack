using UnityEngine;
using HoloToolkit.Unity.InputModule;

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

    private void Update()
    {
        if(controller.previousState == ReversePlayAnimation.ANIM_STATE.REVERSE && controller.State == ReversePlayAnimation.ANIM_STATE.REVERSED && !targetPluck.isPlucked)
        {
            targetMesh.material = materials[Random.Range(0, materials.Length)];        
        }
    }
}
