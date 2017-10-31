using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ReversePlayAnimation))]
public class TicketSwap : MonoBehaviour {
    public Renderer targetMesh;
    ReversePlayAnimation controller;
    IEnumerator coroutine;

    private void Awake()
    {
        controller = GetComponent<ReversePlayAnimation>();
    }

    public void SwapTicketWith(Material material)
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        coroutine = Swap(material);
        StartCoroutine(coroutine);
    }

    IEnumerator Swap(Material material)
    {
        if(controller.State != ReversePlayAnimation.ANIM_STATE.REVERSED)
        {
            controller.ReverseAnimation();
            while(controller.State != ReversePlayAnimation.ANIM_STATE.REVERSED)
            {
                yield return null;
            }
        }

        targetMesh.material = material;
        controller.PlayAnimation();
        yield break;
    }
}
