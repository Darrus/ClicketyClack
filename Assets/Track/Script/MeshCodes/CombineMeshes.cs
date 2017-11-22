/** 
*  @file    CombineMeshes.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief   Contain class CombineMeshes
*
*/
using UnityEngine;
using System.Collections.Generic;

/**
*  @brief A class To Combine multiply Meshes into one mesh
*/
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombineMeshes : MonoBehaviour
{

    /**
    *   @brief A function to Combine multiply Meshes into one mesh
    *  
    *   @param List<GameObject> meshes, a list of Gameobject with the meshs to be Combine
    *   
    *   @return Mesh newMesh
    */
    public static Mesh Combine(List<GameObject> meshes)
    {
        CombineInstance[] combine = new CombineInstance[meshes.Count];

        for (int i = 0; i < meshes.Count; i++)
        {
            MeshFilter filter = meshes[i].GetComponent<MeshFilter>();
            combine[i].mesh = filter.sharedMesh;
            combine[i].transform = filter.transform.localToWorldMatrix;
        }

        Mesh newMesh = new Mesh();
        newMesh.CombineMeshes(combine);

        return newMesh;
    }
}