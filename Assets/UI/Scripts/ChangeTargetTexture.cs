using UnityEngine;

public class ChangeTargetTexture : MonoBehaviour
{
    public Renderer targetMesh;

    public void ChangeTexture(Material material)
    {
        targetMesh.material = material;
    }
}
