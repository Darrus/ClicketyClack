using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CheckGround : MonoBehaviour {
    [TagSelector]
    public string groundTag;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(groundTag))
        {
            LevelManager.TrianOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            LevelManager.TrianOnGround = false;
        }
    }
}
