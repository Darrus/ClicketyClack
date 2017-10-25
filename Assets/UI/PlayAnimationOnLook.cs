using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Animator))]
public class PlayAnimationOnLook : MonoBehaviour
{
    public float lookDistance;

    Collider col;
    Camera mainCam;
    Animator anim;

    bool isLook;

    private void Awake()
    {
        mainCam = Camera.main;
        col = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit);
        if (hit.transform != null)
        {
            if (hit.distance <= lookDistance && hit.collider == col)
            {
                if(!isLook)
                {
                    isLook = true;
                    anim.SetFloat("speedMultiplier", 1.0f);
                }
            }
        }
        else if (isLook)
        {
            isLook = false;
            anim.SetFloat("speedMultiplier", -1.0f);
        }
    }

    public void SetAnimationSpeed(float speed)
    {
        anim.SetFloat("speedMultiplier", speed);
    }
}
