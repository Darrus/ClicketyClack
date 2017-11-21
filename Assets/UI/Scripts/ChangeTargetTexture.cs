/** 
 *  @file     ChangeTargetTexture.cs
 *  @author Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the Change target texture class
 */
using UnityEngine;

/** 
 *  @brief   Change target texture of the renderer
 */
public class ChangeTargetTexture : MonoBehaviour
{
    public Renderer targetMesh;

    /** 
      *  @brief   Change target mesh texture to provided material
      *  @param  material, the given material to replace the target texture
      */
    public void ChangeTexture(Material material)
    {
        targetMesh.material = material;
    }
}
